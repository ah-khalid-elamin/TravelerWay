using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using TravelerWay.Common.Entities;
using TravelerWay.Common.Payloads;

namespace TravelerWay.Common.Interfaces.Implementations
{
    public class StripeService : IStripeService
    {
        private readonly string _secretKey;
        private readonly string _successUrl;
        private readonly string _cancelUrl;

        public StripeService(IConfiguration configuration)
        {
            _secretKey = configuration["Stripe:SecretKey"] ?? throw new ArgumentNullException("Stripe:SecretKey configuration is required");
            _successUrl = configuration["Stripe:SuccessUrl"] ?? throw new ArgumentNullException("Stripe:SuccessUrl configuration is required");
            _cancelUrl = configuration["Stripe:CancelUrl"] ?? throw new ArgumentNullException("Stripe:CancelUrl configuration is required");
        }

        public async Task<CheckoutSessionResponse?> CreateCheckoutSessionAsync(CheckoutSessionRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (request is null) throw new ArgumentNullException(nameof(request));
                if (string.IsNullOrWhiteSpace(_secretKey)) throw new InvalidOperationException("Stripe secret key is not configured.");

                var form = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                {
                    ["mode"] = request.Mode ?? "payment",
                    ["success_url"] = _successUrl,
                    ["cancel_url"] = _cancelUrl
                };

                // Line items
                if (request.LineItems != null)
                {
                    for (var i = 0; i < request.LineItems.Count; i++)
                    {
                        var li = request.LineItems[i];
                        var prefix = $"line_items[{i}]";

                        if (li.Quantity.HasValue)
                            form[$"{prefix}[quantity]"] = li.Quantity.Value;

                        if (li.PriceData != null)
                        {
                            var pd = li.PriceData;
                            var pdPrefix = $"{prefix}[price_data]";

                            if (!string.IsNullOrWhiteSpace(pd.Currency))
                                form[$"{pdPrefix}[currency]"] = pd.Currency;

                            if (pd.UnitAmount.HasValue)
                                form[$"{pdPrefix}[unit_amount]"] = pd.UnitAmount.Value;

                            if (pd.ProductData != null)
                            {
                                var prod = pd.ProductData;
                                var prodPrefix = $"{pdPrefix}[product_data]";

                                if (!string.IsNullOrWhiteSpace(prod.Name))
                                    form[$"{prodPrefix}[name]"] = prod.Name;

                                if (!string.IsNullOrWhiteSpace(prod.Description))
                                    form[$"{prodPrefix}[description]"] = prod.Description;

                                if (prod.Images != null)
                                {
                                    for (var j = 0; j < prod.Images.Count; j++)
                                    {
                                        form[$"{prodPrefix}[images][{j}]"] = prod.Images[j];
                                    }
                                }
                            }
                        }
                    }
                }

                // Metadata
                if (request.Metadata != null)
                {
                    foreach (var kvp in request.Metadata)
                    {
                        if (kvp.Key is not null && kvp.Value is not null)
                            form[$"metadata[{kvp.Key}]"] = kvp.Value;
                    }
                }

                var response = await "https://api.stripe.com/v1/checkout/sessions"
                    .WithHeader("Idempotency-Key", request.IdempotencyKey)
                    .WithBasicAuth(_secretKey, string.Empty)
                    .PostUrlEncodedAsync(form, cancellationToken: cancellationToken)
                    .ReceiveJson<CheckoutSessionResponse>();

                return response;
            }
            catch (FlurlHttpException ex)
            {
                // Log the error or handle it as needed
                var errorResponse = await ex.GetResponseStringAsync();
                throw new InvalidOperationException($"Stripe API request failed: {errorResponse}", ex);
            }

            catch (Exception ex)
            {
                // Log the error or handle it as needed
                throw new InvalidOperationException($"Stripe API request failed: {ex.Message}", ex);
            }
        }
    }
}
