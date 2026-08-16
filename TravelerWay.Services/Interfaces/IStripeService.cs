using TravelerWay.Common.Models;

namespace TravelerWay.Services.Interfaces;

public interface IStripeService
{
    Task<CheckoutSessionResponse> CreateCheckoutSessionAsync(CheckoutSessionRequest request, CancellationToken cancellationToken = default);
}
