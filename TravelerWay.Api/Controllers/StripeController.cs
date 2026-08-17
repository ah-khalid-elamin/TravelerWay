using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using TravelerWay.Common.Data.Repositories;
using TravelerWay.Common.Entities;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Payloads;

namespace TravelerWay.Api.Controllers;

[ApiController]
[Route("api/stripe")]
public class StripeController : ControllerBase
{
    private readonly ILogger<StripeController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ITravelerWayService _travelerWayService;
    private readonly INotificationService _notificationService;
    private readonly IStripeEventLogRepository _stripeEventLogRepository;

    public StripeController(ILogger<StripeController> logger, IConfiguration configuration, ITravelerWayService travelerWayService, INotificationService notificationService, IStripeEventLogRepository stripeEventLogRepository)
    {
        _logger = logger;
        _configuration = configuration;
        _travelerWayService = travelerWayService;
        _notificationService = notificationService;
        _stripeEventLogRepository = stripeEventLogRepository;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Handle(
    [FromHeader(Name = "Stripe-Signature")] string stripeSignature,
    CancellationToken cancellationToken)
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync(cancellationToken);
        var endpointSecret = _configuration["Stripe:WebhookSecret"];

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, endpointSecret);
            _logger.LogInformation("Stripe event received: {EventType}", stripeEvent.Type);

            var existingEventLog = await _stripeEventLogRepository.GetEventLogByEventIdAndNameAsync(stripeEvent.Id, stripeEvent.Type);

            if (existingEventLog != null) return Ok(); // Event already processed, return early

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;
                _logger.LogInformation("Payment succeeded for session {SessionId}", session?.Id);

                var offerId = session?.Metadata.FirstOrDefault().Value;

                var eventLog = new StripeEventLog
                {
                    Id = Guid.NewGuid(),
                    StripeEventId = stripeEvent.Id,
                    EventName = stripeEvent.Type,
                    OfferId = offerId,
                    ReceivedAt = DateTime.UtcNow
                };

                await _stripeEventLogRepository.AddAsync(eventLog);
                await _stripeEventLogRepository.SaveChangesAsync();

                var order = await _travelerWayService.CreateOrderWithBalanceAsync(offerId!);
            }

            return Ok();
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Stripe webhook signature verification failed.");
            return BadRequest();
        }
    }

    [HttpGet("success")]
    public IActionResult Success()
    {
        const string html = """
    <!doctype html>
    <html lang="en">
    <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Payment Successful — TravelerWay</title>
    <style>
        :root {
            --accent: #0f766e;
            --accent-light: #14b8a6;
            --bg: #f8faf9;
            --text: #1a2e2b;
            --muted: #5c6b68;
        }
        * { box-sizing: border-box; margin: 0; padding: 0; }
        body {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            background: linear-gradient(160deg, #f0fdf9 0%, var(--bg) 60%);
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif;
            color: var(--text);
            padding: 24px;
        }
        .card {
            background: #fff;
            border-radius: 20px;
            padding: 48px 40px;
            max-width: 420px;
            width: 100%;
            text-align: center;
            box-shadow: 0 20px 60px -15px rgba(15, 118, 110, 0.25), 0 4px 12px rgba(0,0,0,0.04);
            animation: rise 0.5s ease-out;
        }
        @keyframes rise {
            from { opacity: 0; transform: translateY(12px); }
            to { opacity: 1; transform: translateY(0); }
        }
        .icon-wrap {
            width: 76px; height: 76px;
            margin: 0 auto 24px;
            border-radius: 50%;
            background: linear-gradient(135deg, var(--accent-light), var(--accent));
            display: flex; align-items: center; justify-content: center;
            box-shadow: 0 8px 20px -4px rgba(15, 118, 110, 0.4);
        }
        .icon-wrap svg { width: 36px; height: 36px; }
        .check-path {
            stroke-dasharray: 40;
            stroke-dashoffset: 40;
            animation: draw 0.5s 0.3s ease-out forwards;
        }
        @keyframes draw { to { stroke-dashoffset: 0; } }
        h1 { font-size: 22px; font-weight: 700; margin-bottom: 10px; letter-spacing: -0.01em; }
        p { color: var(--muted); font-size: 15px; line-height: 1.6; margin-bottom: 28px; }
        .badge {
            display: inline-block;
            font-size: 12px; font-weight: 600;
            color: var(--accent);
            background: #ecfdf5;
            border: 1px solid #d1fae5;
            padding: 6px 14px;
            border-radius: 999px;
            margin-bottom: 24px;
            letter-spacing: 0.02em;
        }
        .close-note {
            font-size: 13px;
            color: #9aa5a3;
            border-top: 1px solid #eef1f0;
            padding-top: 20px;
        }
        .brand {
            font-size: 12px;
            color: #b8c0be;
            margin-top: 20px;
            letter-spacing: 0.05em;
            text-transform: uppercase;
        }
    </style>
    </head>
    <body>
        <div class="card">
            <div class="badge">Booking Confirmed</div>
            <div class="icon-wrap">
                <svg viewBox="0 0 24 24" fill="none">
                    <path class="check-path" d="M5 13l4 4L19 7" stroke="#fff" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"/>
                </svg>
            </div>
            <h1>Payment Successful</h1>
            <p>Thank you — your payment has been received and your booking is being finalized. You'll get a confirmation shortly on the channel you booked through.</p>
            <div class="close-note">You may safely close this window now.</div>
            <div class="brand">TravelerWay</div>
        </div>
    </body>
    </html>
    """;
        return Content(html, "text/html");
    }

    [HttpGet("cancel")]
    public IActionResult Cancel()
    {
        const string html = """
    <!doctype html>
    <html lang="en">
    <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Payment Status — TravelerWay</title>
    <style>
        :root {
            --accent: #b45309;
            --accent-light: #f59e0b;
            --bg: #fdfaf6;
            --text: #2e2418;
            --muted: #6b6055;
        }
        * { box-sizing: border-box; margin: 0; padding: 0; }
        body {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            background: linear-gradient(160deg, #fffbeb 0%, var(--bg) 60%);
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif;
            color: var(--text);
            padding: 24px;
        }
        .card {
            background: #fff;
            border-radius: 20px;
            padding: 48px 40px;
            max-width: 420px;
            width: 100%;
            text-align: center;
            box-shadow: 0 20px 60px -15px rgba(180, 83, 9, 0.2), 0 4px 12px rgba(0,0,0,0.04);
            animation: rise 0.5s ease-out;
        }
        @keyframes rise {
            from { opacity: 0; transform: translateY(12px); }
            to { opacity: 1; transform: translateY(0); }
        }
        .icon-wrap {
            width: 76px; height: 76px;
            margin: 0 auto 24px;
            border-radius: 50%;
            background: linear-gradient(135deg, var(--accent-light), var(--accent));
            display: flex; align-items: center; justify-content: center;
            box-shadow: 0 8px 20px -4px rgba(180, 83, 9, 0.35);
        }
        .icon-wrap svg { width: 34px; height: 34px; }
        .badge {
            display: inline-block;
            font-size: 12px; font-weight: 600;
            color: var(--accent);
            background: #fef3c7;
            border: 1px solid #fde68a;
            padding: 6px 14px;
            border-radius: 999px;
            margin-bottom: 24px;
            letter-spacing: 0.02em;
        }
        h1 { font-size: 22px; font-weight: 700; margin-bottom: 10px; letter-spacing: -0.01em; }
        p { color: var(--muted); font-size: 15px; line-height: 1.6; margin-bottom: 28px; }
        .retry-note {
            font-size: 13px;
            color: #a8a094;
            border-top: 1px solid #f2ede4;
            padding-top: 20px;
        }
        .brand {
            font-size: 12px;
            color: #cbbfa9;
            margin-top: 20px;
            letter-spacing: 0.05em;
            text-transform: uppercase;
        }
    </style>
    </head>
    <body>
        <div class="card">
            <div class="badge">Action Needed</div>
            <div class="icon-wrap">
                <svg viewBox="0 0 24 24" fill="none">
                    <path d="M12 8v5M12 16.5h.01" stroke="#fff" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"/>
                    <circle cx="12" cy="12" r="9" stroke="#fff" stroke-width="2"/>
                </svg>
            </div>
            <h1>Payment Failed</h1>
            <p>Your payment was canceled or needs additional action. No charge was made — you can return and try again whenever you're ready.</p>
            <div class="retry-note">You may close this window and return to continue your booking.</div>
            <div class="brand">TravelerWay</div>
        </div>
    </body>
    </html>
    """;
        return Content(html, "text/html");
    }
}
