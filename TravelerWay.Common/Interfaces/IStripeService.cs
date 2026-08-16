using TravelerWay.Common.Entities;
using TravelerWay.Common.Payloads;

namespace TravelerWay.Common.Interfaces;

public interface IStripeService
{
    Task<CheckoutSessionResponse?> CreateCheckoutSessionAsync(CheckoutSessionRequest request, CancellationToken cancellationToken = default);
}
