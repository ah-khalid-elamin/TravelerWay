using System;
using TravelerWay.Common.Payloads;

namespace TravelerWay.Common.Interfaces;

public interface ITravelerWayService
{
    Task<TravelerWaysSearchResponse> SearchOffers(DuffelOfferReqRequest offerReqRequest,
        bool returnOffers = false,
        int? limit = null,
        string? sort = null,
        bool? returnAvailableServices = null,
        bool? requiresInstantPayment = null);

    Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferResponse>>> ListOffersAsync(string offerRequestId, int? limit = null, int? maxConnections = null, string? sort = null, bool? requiresInstantPayment = null);
    Task<DuffelOfferResponse?> GetOfferAsync(string offerId, bool? returnAvailableServices = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<DuffelOfferService>?> ListOfferAvailableServicesAsync(string offerId, bool? returnAvailableServices = null);
    Task<DuffelOfferResponse> AddServiceAsync(string offerId, string serviceId, int quantity = 1);

    Task<CheckoutSessionResponse?> GeneratePaymentLinkAsync(string offerId, CancellationToken cancellationToken = default);

    Task<DuffelPassengerResponse?> UpdatePassengerAsync(string offerId, string offerPassengerId, DuffelPassengerRequest request, CancellationToken cancellationToken = default);
    Task<DuffelOrderResponse?> CreateOrderWithBalanceAsync(string offerId, CancellationToken cancellationToken = default);

}
