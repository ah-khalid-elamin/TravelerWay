using TravelerWay.Common.Entities;
using TravelerWay.Common.Payloads;

namespace TravelerWay.Common.Interfaces;

public interface IDuffelService
{
    Task<DuffelCustomerResponse?> CreateCustomerAsync(DuffelCustomerRequest customer, CancellationToken cancellationToken = default);
    Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelCustomerResponse>>> ListCustomersAsync(string? email, int? limit = null, string? before = null, string? after = null, CancellationToken cancellationToken = default);
    Task<DuffelCustomerResponse?> GetCustomerAsync(string id, CancellationToken cancellationToken = default);
    Task<DuffelCustomerResponse?> UpdateCustomerAsync(string id, DuffelCustomerRequest customerUpdate, CancellationToken cancellationToken = default);

    Task<DuffelOfferReqResponse?> CreateOfferRequestAsync(DuffelOfferReqRequest offer, bool returnOffers, CancellationToken cancellationToken = default);
    Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferReqResponse>>> ListOfferRequestsAsync(int? limit = null, string? before = null, string? after = null, string? sort = null, CancellationToken cancellationToken = default);    
    Task<DuffelOfferReqResponse?> GetOfferRequestAsync(string id, CancellationToken cancellationToken = default);
    Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferResponse>>> ListOffersAsync(string offerRequestId, int? limit = null, int? maxConnections = null, string? sort = null, bool? requiresInstantPayment = null, CancellationToken cancellationToken = default);
    Task<DuffelOfferResponse?> GetOfferAsync(string offerId, bool? returnAvailableServices = null, CancellationToken cancellationToken = default);
    Task<DuffelOfferResponse> PriceOfferAsync(string offerId, DuffelOfferPricingRequest request);
    Task<DuffelPassengerResponse?> UpdatePassengerAsync(string offerId, string offerPassengerId, DuffelPassengerRequest request, CancellationToken cancellationToken);

    Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderResponse>>> ListOrdersAsync(
         int? limit = null,
         string? before = null,
         string? after = null,
         string? bookingReference = null,
         string? offerId = null,
         bool? awaitingPayment = null,
         string? sort = null,
         IEnumerable<string>? ownerIds = null,
         IEnumerable<string>? originIds = null,
         IEnumerable<string>? destinationIds = null,
         string? departingAt = null,
         string? arrivingAt = null,
         string? createdAt = null,
         IEnumerable<string>? passengerNames = null,
         bool? requiresAction = null,
         string? userId = null,
         CancellationToken cancellationToken = default);

    Task<DuffelOrderResponse?> GetOrderAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<DuffelOrderService>> ListOrderAvailableServicesAsync(string orderId, CancellationToken cancellationToken);
    Task<DuffelOrderResponse?> CreateOrderAsync(DuffelOrderRequest orderRequest, CancellationToken cancellationToken);
    Task<DuffelOrderResponse?> AddServicesToOrderAsync(string orderId, DuffelOrderServiceAdditionRequest additionRequest, CancellationToken cancellationToken = default);   
    Task<DuffelOrderResponse?> UpdateOrderAsync(string orderId, DuffelUpdateOrderRequest orderUpdate, CancellationToken cancellationToken = default);

    Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderCancellationResponse>>> ListOrderCancellationsAsync(string? orderId = null, int? limit = null, string? before = null, string? after = null, CancellationToken cancellationToken = default);
    Task<DuffelOrderCancellationResponse?> CreateOrderCancellationAsync(DuffelOrderCancellationRequest request, CancellationToken cancellationToken = default);
    Task<DuffelOrderCancellationResponse?> GetOrderCancellationAsync(string id, CancellationToken cancellationToken = default);
    Task<DuffelOrderCancellationResponse?> ConfirmOrderCancellationAsync(string id, CancellationToken cancellationToken = default);

    Task<DuffelOrderChangeReqResponse?> CreateOrderChangeRequestAsync(DuffelOrderChangeReqRequest orderChangeRequest, CancellationToken cancellationToken = default);
    Task<DuffelOrderChangeReqResponse?> GetOrderChangeRequestAsync(string id, CancellationToken cancellationToken = default);
    Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderChangeOffer>>> ListOrderChangeOffersAsync(
        string orderChangeRequestId,
        int? limit = null,
        string? before = null,
        string? after = null,
        string? sort = null,
        int? maxConnections = null,
        CancellationToken cancellationToken = default);

    Task<DuffelOrderChangeOffer?> GetOrderChangeOfferAsync(string id, CancellationToken cancellationToken = default);
    Task<DuffelOrderChange?> CreateOrderChangeAsync(string selectedOrderChangeOfferId, CancellationToken cancellationToken);
    Task<DuffelOrderChange?> GetOrderChangeAsync(string id, CancellationToken cancellationToken);
    Task<DuffelOrderChange?> ConfirmOrderChangeAsync(string id, DuffelPayment payment, CancellationToken cancellationToken);
}
