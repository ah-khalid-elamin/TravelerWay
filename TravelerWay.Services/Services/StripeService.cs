using Stripe;
using Stripe.Checkout;
using TravelerWay.Common.Models;
using TravelerWay.Services.Interfaces;

namespace TravelerWay.Services.Services;

public class StripeService : IStripeService
{
    private readonly StripeClient _stripeClient;

    public StripeService(string apiKey)
    {
        _stripeClient = new StripeClient(apiKey);
    }

    public async Task<CheckoutSessionResponse> CreateCheckoutSessionAsync(CheckoutSessionRequest request, CancellationToken cancellationToken = default)
    {
        var service = new SessionService(_stripeClient);
        var options = new SessionCreateOptions
        {
            Mode = "payment",
            SuccessUrl = request.SuccessUrl,
            CancelUrl = request.CancelUrl,
            LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(request.Amount * 100),
                        Currency = request.Currency.ToLowerInvariant(),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"TravelerWay booking {request.BookingReference}"
                        }
                    },
                    Quantity = 1
                }
            }
        };

        var session = await service.CreateAsync(options, cancellationToken: cancellationToken);
        return new CheckoutSessionResponse { Id = session.Id, Url = session.Url }; 
    }
}
