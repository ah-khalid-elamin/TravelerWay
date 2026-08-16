using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TravelerWay.Common.Entities;
using TravelerWay.Common.Exceptions;
using TravelerWay.Common.Payloads;


namespace TravelerWay.Common.Interfaces.Implementations;

public class DuffelService : IDuffelService
{
    private readonly HttpClient _httpClient;
    private readonly string _accessToken;

    public DuffelService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _accessToken = configuration["Duffel:AccessToken"] ?? string.Empty;
    }

    public async Task<DuffelCustomerResponse?> CreateCustomerAsync(DuffelCustomerRequest customer, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrEmpty(_accessToken))
                throw new InvalidOperationException("Duffel access token is not configured.");

            const string url = "https://api.duffel.com/identity/customer/users";

            var response = await url
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { data = customer }, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelCustomerResponse>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelCustomerResponse>>> ListCustomersAsync(string? email, int? limit = null, string? before = null, string? after = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await "https://api.duffel.com/identity/customer/users"
          .SetQueryParams(new { email, limit, before, after }) // Automatically ignores null/empty values
          .WithOAuthBearerToken(_accessToken)
          .WithHeader("Duffel-Version", "v2")
          .WithHeader("Accept", "application/json")
          .GetJsonAsync<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelCustomerResponse>>>(cancellationToken: cancellationToken);

            return response;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelCustomerResponse?> GetCustomerAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Customer id must be provided", nameof(id));

            var response = await $"https://api.duffel.com/identity/customer/users/{id}"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .GetJsonAsync<DuffelResponse<DuffelCustomerResponse>>(cancellationToken: cancellationToken);

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelCustomerResponse?> UpdateCustomerAsync(string id, DuffelCustomerRequest customerUpdate, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Customer id must be provided", nameof(id));
            if (customerUpdate == null)
                throw new ArgumentNullException(nameof(customerUpdate));

            var payload = new { data = customerUpdate };

            var response = await $"https://api.duffel.com/identity/customer/users/{id}"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .PutJsonAsync(payload, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelCustomerResponse>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOfferReqResponse?> CreateOfferRequestAsync(DuffelOfferReqRequest offerRequest, bool returnOffers, CancellationToken cancellationToken = default)
    {
        try
        {
            if (offerRequest == null)
                throw new ArgumentNullException(nameof(offerRequest));

            const string url = "https://api.duffel.com/air/offer_requests";

            var response = await url
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .SetQueryParam("return_offers", returnOffers.ToString().ToLower())
                .PostJsonAsync(new { data = offerRequest }, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelOfferReqResponse>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferReqResponse>>> ListOfferRequestsAsync(int? limit = null, string? before = null, string? after = null, string? sort = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await "https://api.duffel.com/air/offer_requests"
                .SetQueryParams(new { limit, before, after, sort })
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .GetJsonAsync<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferReqResponse>>>(cancellationToken: cancellationToken);

            return response ?? new DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferReqResponse>>(new DuffelPaginationFilters(), Array.Empty<DuffelOfferReqResponse>());
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOfferReqResponse?> GetOfferRequestAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Offer request id must be provided", nameof(id));

            var response = await $"https://api.duffel.com/air/offer_requests/{id}"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .GetJsonAsync<DuffelResponse<DuffelOfferReqResponse>>(cancellationToken: cancellationToken);

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferResponse>>> ListOffersAsync(string offerRequestId,
        int? limit = null,
        int? maxConnections = null,
        string? sort = null,
        bool? requiresInstantPayment = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(offerRequestId))
                throw new ArgumentException("Offer request id must be provided", nameof(offerRequestId));

            var response = await "https://api.duffel.com/air/offers"
                .SetQueryParams(new { limit, max_connections = maxConnections, sort, requires_instant_payment = requiresInstantPayment, offer_request_id = offerRequestId })
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .GetJsonAsync<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferResponse>>>(cancellationToken: cancellationToken);

            return response ?? new DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferResponse>>(new DuffelPaginationFilters(), Array.Empty<DuffelOfferResponse>());
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOfferResponse?> GetOfferAsync(string offerId, bool? returnAvailableServices = null, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(offerId))
                throw new ArgumentException("Offer id must be provided", nameof(offerId));

            var response = await $"https://api.duffel.com/air/offers/{offerId}"
                .SetQueryParams(new { return_available_services = returnAvailableServices?.ToString().ToLower() })
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .GetJsonAsync<DuffelResponse<DuffelOfferResponse>>(cancellationToken: cancellationToken);

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }

        catch (Exception ex)
        {
            throw new InvalidOperationException($"Duffel API request failed: {ex.Message}", ex);
        }
    }

    public async Task<DuffelOfferResponse> PriceOfferAsync(string offerId, DuffelOfferPricingRequest request)
    {
        try
        {
            var url = $"https://api.duffel.com/air/offers/{offerId}/actions/price";
            var response = await url
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { data = request })
                .ReceiveJson<DuffelResponse<DuffelOfferResponse>>();

            return response.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelPassengerResponse?> UpdatePassengerAsync(string offerId, string offerPassengerId, DuffelPassengerRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrEmpty(_accessToken))
                throw new InvalidOperationException("Duffel access token is not configured.");

            var url = $"https://api.duffel.com/air/offers/{offerId}/passengers/{offerPassengerId}";

            var response = await url
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .PatchJsonAsync(new { data = request }, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelPassengerResponse>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderResponse>>> ListOrdersAsync(
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
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrEmpty(_accessToken))
                throw new InvalidOperationException("Duffel access token is not configured.");

            var query = new Dictionary<string, object?>();
            if (limit.HasValue) query["limit"] = limit.Value;
            if (!string.IsNullOrEmpty(before)) query["before"] = before;
            if (!string.IsNullOrEmpty(after)) query["after"] = after;
            if (!string.IsNullOrEmpty(bookingReference)) query["booking_reference"] = bookingReference;
            if (!string.IsNullOrEmpty(offerId)) query["offer_id"] = offerId;
            if (awaitingPayment.HasValue) query["awaiting_payment"] = awaitingPayment.Value.ToString().ToLower();
            if (!string.IsNullOrEmpty(sort)) query["sort"] = sort;
            if (ownerIds != null && ownerIds.Any()) query["owner_id[]"] = ownerIds;
            if (originIds != null && originIds.Any()) query["origin_id[]"] = originIds;
            if (destinationIds != null && destinationIds.Any()) query["destination_id[]"] = destinationIds;
            if (!string.IsNullOrEmpty(departingAt)) query["departing_at"] = departingAt;
            if (!string.IsNullOrEmpty(arrivingAt)) query["arriving_at"] = arrivingAt;
            if (!string.IsNullOrEmpty(createdAt)) query["created_at"] = createdAt;
            if (passengerNames != null && passengerNames.Any()) query["passenger_name[]"] = passengerNames;
            if (requiresAction.HasValue) query["requires_action"] = requiresAction.Value.ToString().ToLower();
            if (!string.IsNullOrEmpty(userId)) query["user_id"] = userId;

            var response = await "https://api.duffel.com/air/orders"
                .SetQueryParams(query)
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .WithHeader("Accept-Encoding", "gzip")
                .GetJsonAsync<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderResponse>>>(cancellationToken: cancellationToken);

            return response ?? new DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderResponse>>(new DuffelPaginationFilters(), Array.Empty<DuffelOrderResponse>());
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderResponse?> GetOrderAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Order id must be provided.", nameof(id));


            var url = $"https://api.duffel.com/air/orders/{id}";

            var order = await url
                .WithHeader("Accept-Encoding", "gzip")
                .WithHeader("Accept", "application/json")
                .WithHeader("Duffel-Version", "v2")
                .WithOAuthBearerToken(_accessToken)
                .GetJsonAsync<DuffelResponse<DuffelOrderResponse>>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return order?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<IEnumerable<DuffelOrderService>> ListOrderAvailableServicesAsync(string orderId, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(orderId))
                throw new ArgumentException("Order id must be provided", nameof(orderId));

            var response = await $"https://api.duffel.com/air/orders/{orderId}/available_services"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .GetJsonAsync<DuffelResponse<List<DuffelOrderService>>>(cancellationToken: cancellationToken);

            return response?.Data ?? new List<DuffelOrderService>();
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderResponse?> CreateOrderAsync(DuffelOrderRequest orderRequest, CancellationToken cancellationToken)
    {
        try
        {
            if (orderRequest == null)
                throw new ArgumentException("order request must be provided", nameof(orderRequest));

            const string url = "https://api.duffel.com/air/orders";

            var response = await url
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { data = orderRequest }, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelOrderResponse>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderResponse?> AddServicesToOrderAsync(string orderId, DuffelOrderServiceAdditionRequest additionRequest, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(orderId))
                throw new ArgumentException("Order id must be provided", nameof(orderId));
            if (additionRequest == null)
                throw new ArgumentNullException(nameof(additionRequest));
            if (string.IsNullOrEmpty(_accessToken))
                throw new InvalidOperationException("Duffel access token is not configured.");

            var response = await $"https://api.duffel.com/air/orders/{orderId}/services"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { data = additionRequest }, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelOrderResponse>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderResponse?> UpdateOrderAsync(string orderId, DuffelUpdateOrderRequest orderUpdate, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(orderId))
                throw new ArgumentException("Order id must be provided", nameof(orderId));
            if (orderUpdate == null)
                throw new ArgumentNullException(nameof(orderUpdate));

            var response = await $"https://api.duffel.com/air/orders/{orderId}"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .PatchJsonAsync(new { data = orderUpdate }, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelOrderResponse>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderCancellationResponse>>> ListOrderCancellationsAsync(string? orderId = null, int? limit = null, string? before = null, string? after = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await "https://api.duffel.com/air/order_cancellations"
                .SetQueryParams(new { after, before, limit, order_id = orderId })
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .GetJsonAsync<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderCancellationResponse>>>(cancellationToken: cancellationToken);

            return response ?? new DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderCancellationResponse>>(new DuffelPaginationFilters(), Array.Empty<DuffelOrderCancellationResponse>());
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderCancellationResponse?> CreateOrderCancellationAsync(DuffelOrderCancellationRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            const string url = "https://api.duffel.com/air/order_cancellations";

            var payload = new { data = request };

            var response = await url
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(payload, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelOrderCancellationResponse>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderCancellationResponse?> GetOrderCancellationAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Order cancellation id must be provided", nameof(id));

            var response = await $"https://api.duffel.com/air/order_cancellations/{id}"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .WithHeader("Accept-Encoding", "gzip")
                .GetJsonAsync<DuffelResponse<DuffelOrderCancellationResponse>>(cancellationToken: cancellationToken);

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderCancellationResponse?> ConfirmOrderCancellationAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Order cancellation id must be provided", nameof(id));


            var response = await $"https://api.duffel.com/air/order_cancellations/{id}/actions/confirm"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { }, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelOrderCancellationResponse>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderChangeReqResponse?> CreateOrderChangeRequestAsync(DuffelOrderChangeReqRequest orderChangeRequest, CancellationToken cancellationToken = default)
    {
        try
        {
            if (orderChangeRequest == null)
                throw new ArgumentNullException(nameof(orderChangeRequest));
            if (string.IsNullOrEmpty(_accessToken))
                throw new InvalidOperationException("Duffel access token is not configured.");

            const string url = "https://api.duffel.com/air/order_change_requests";

            var response = await url
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(new { data = orderChangeRequest }, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelOrderChangeReqResponse>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderChangeReqResponse?> GetOrderChangeRequestAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Order change request id must be provided", nameof(id));

            var response = await $"https://api.duffel.com/air/order_change_requests/{id}"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .WithHeader("Accept", "application/json")
                .WithHeader("Accept-Encoding", "gzip")
                .GetJsonAsync<DuffelResponse<DuffelOrderChangeReqResponse>>(cancellationToken: cancellationToken);

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderChangeOffer>>> ListOrderChangeOffersAsync(
        string orderChangeRequestId,
        int? limit = null,
        string? before = null,
        string? after = null,
        string? sort = null,
        int? maxConnections = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(orderChangeRequestId))
                throw new ArgumentException("Order change request id must be provided", nameof(orderChangeRequestId));

            var response = await "https://api.duffel.com/air/order_change_offers"
                .SetQueryParams(new
                {
                    after,
                    before,
                    limit,
                    order_change_request_id = orderChangeRequestId,
                    sort,
                    max_connections = maxConnections
                })
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .GetJsonAsync<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderChangeOffer>>>(cancellationToken: cancellationToken);

            return response ?? new DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOrderChangeOffer>>(new DuffelPaginationFilters(), Array.Empty<DuffelOrderChangeOffer>());
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderChangeOffer?> GetOrderChangeOfferAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Order change offer id must be provided", nameof(id));

            var response = await $"https://api.duffel.com/air/order_change_offers/{id}"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .GetJsonAsync<DuffelResponse<DuffelOrderChangeOffer>>(cancellationToken: cancellationToken);

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderChange?> CreateOrderChangeAsync(string selectedOrderChangeOfferId, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(selectedOrderChangeOfferId))
                throw new ArgumentException("Selected order change offer id must be provided", nameof(selectedOrderChangeOfferId));

            const string url = "https://api.duffel.com/air/order_changes";

            var payload = new { data = new { selected_order_change_offer = selectedOrderChangeOfferId } };

            var response = await url
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .PostJsonAsync(payload, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelOrderChange>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderChange?> GetOrderChangeAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Order change id must be provided", nameof(id));

            var response = await $"https://api.duffel.com/air/order_changes/{id}"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .GetJsonAsync<DuffelResponse<DuffelOrderChange>>(cancellationToken: cancellationToken);

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

    public async Task<DuffelOrderChange?> ConfirmOrderChangeAsync(string id, DuffelPayment payment, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Order change id must be provided", nameof(id));
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));

            var payload = new { data = new { payment } };

            var response = await $"https://api.duffel.com/air/order_changes/{id}/actions/confirm"
                .WithOAuthBearerToken(_accessToken)
                .WithHeader("Duffel-Version", "v2")
                .PostJsonAsync(payload, cancellationToken: cancellationToken)
                .ReceiveJson<DuffelResponse<DuffelOrderChange>>();

            return response?.Data;
        }
        catch (FlurlHttpException ex)
        {
            var errorResponse = await ex.GetResponseStringAsync();
            throw new DuffelException(ex.StatusCode, "Duffel API", ex.Message);
        }
    }

}
