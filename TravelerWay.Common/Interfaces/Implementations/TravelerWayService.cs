using Flurl.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Stripe.Climate;
using System;
using System.Text.Json;
using TravelerWay.Api.Data;
using TravelerWay.Common.Data.Repositories;
using TravelerWay.Common.Entities;
using TravelerWay.Common.Exceptions;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Payloads;

namespace TravelerWay.Common.Interfaces.Implementations;

public class TravelerWayService : ITravelerWayService
{
    private readonly IDuffelService _duffelService;
    private readonly IStripeService _stripeService;
    private readonly INotificationService _notificationService;
    private readonly ILogger<TravelerWayService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly ISearchRepository _searchRepository;
    private readonly IOfferRepository _offerRepository;
    private readonly IAncillaryRepository _ancillaryRepository;
    private readonly IPassengerRepository _passengerRepository;
    private readonly IPaymentRepository _paymentRepository;

    public TravelerWayService(IDuffelService duffelService, 
        IStripeService stripeService,
        INotificationService notificationService, 
        ILogger<TravelerWayService> logger, 
        IUserRepository userRepository, 
        ISearchRepository searchRepository, 
        IOfferRepository offerRepository,
        IAncillaryRepository ancillaryRepository,
        IPassengerRepository passengerRepository,
        IPaymentRepository paymentRepository
        )
    {
        _duffelService = duffelService;
        _stripeService = stripeService;
        _notificationService = notificationService;
        _logger = logger;
        _userRepository = userRepository;
        _searchRepository = searchRepository;
        _offerRepository = offerRepository;
        _ancillaryRepository = ancillaryRepository;
        _passengerRepository = passengerRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<TravelerWaysSearchResponse> SearchOffers(DuffelOfferReqRequest offerReqRequest,
        bool returnOffers = false,
        int? limit = null,
        string? sort = null,
        bool? returnAvailableServices = null,
        bool? requiresInstantPayment = null)
    {


        if (offerReqRequest is null) throw new Exception("Invalid Search request: no search details were provided.");
        if (offerReqRequest.Passengers.Count() == 0) throw new Exception("Invalid Search request: no passenger details were provided.");

        var offerRequest = await _duffelService.CreateOfferRequestAsync(offerReqRequest, returnOffers);

        if (offerRequest == null) throw new InvalidOperationException("Offer request creation failed.");

        var offers = await _duffelService.ListOffersAsync(offerRequest.Id!,
                    limit,
                    offerReqRequest.MaxConnections,
                    sort,
                    requiresInstantPayment);

        var response = new TravelerWaysSearchResponse() { Meta = offers.Meta, OfferRequestId = offerRequest.Id, Data = offers.Data };

        // Save the user if not already exists in the database
        var userEntity = await _userRepository.GetUserByUsernameAsync(offerReqRequest?.Username!);

        if (userEntity == null)
        {
            userEntity = new User { 
                Id = Guid.NewGuid(),
                ChannelType = offerReqRequest?.ChannelType == "telegram"? ChannelType.Telegram : ChannelType.None ,
                ChatId = offerReqRequest?.ChatId!,
                PhoneNumber = offerReqRequest?.PhoneNumber!,
                Name = offerReqRequest?.Username,
                PreferredLanguage = offerReqRequest?.PreferredLanguage,
                Email = offerReqRequest?.Email!, 
                Username = offerReqRequest?.Username!,
                CreatedAt = DateTime.UtcNow
            };
            await _userRepository.AddAsync(userEntity);
        }

        //Save the search details in the database
        var searchEntity = new Search
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            BookingOfferRequestId = offerRequest.Id,
            Sort = sort,
            MaxConnections = offerReqRequest?.MaxConnections,
            Origin = offerReqRequest?.Slices?.FirstOrDefault()?.Origin,
            Destination = offerReqRequest?.Slices?.FirstOrDefault()?.Destination,
            CabinClass = offerReqRequest?.CabinClass!,
            CreatedAt = DateTime.UtcNow
        };

        await _searchRepository.AddAsync(searchEntity);

        foreach (var offer in offers.Data)
        {
            var offerEntity = new Entities.Offer
            {
                Id = Guid.NewGuid(),
                BookingOfferId = offer.Id,
                SearchId = searchEntity.Id,
                UserId = userEntity.Id,
            };

            await _offerRepository.AddAsync(offerEntity);

        }

        await _userRepository.SaveChangesAsync();
        await _offerRepository.SaveChangesAsync();
        await _searchRepository.SaveChangesAsync();

        return response;

    }

    public async Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferResponse>>> ListOffersAsync(string offerRequestId,
        int? limit = null,
        int? maxConnections = null,
        string? sort = null,
        bool? requiresInstantPayment = null)
    {
        var offers = await _duffelService.ListOffersAsync(offerRequestId, limit, maxConnections, sort, requiresInstantPayment);

        return offers;
    }

    public async Task<DuffelOfferResponse?> GetOfferAsync(string offerId, bool? returnAvailableServices = null, CancellationToken cancellationToken = default)
    {

        if (string.IsNullOrWhiteSpace(offerId))
            throw new ArgumentException("Offer id must be provided", nameof(offerId));

        var offer = await _duffelService.GetOfferAsync(offerId, returnAvailableServices);
        if (offer == null)
            _logger.LogWarning("Offer with id {OfferId} was not found.", offerId);

        return offer;
    }


    public async Task<IEnumerable<DuffelOfferService>?> ListOfferAvailableServicesAsync(string offerId, bool? returnAvailableServices = true)
    {
        if (string.IsNullOrWhiteSpace(offerId))
            throw new ArgumentException("Offer id must be provided", nameof(offerId));

        var offer = await _duffelService.GetOfferAsync(offerId, returnAvailableServices);
        if (offer == null)
            _logger.LogWarning("Offer with id {OfferId} was not found.", offerId);

        return offer?.AvailableServices;
    }

    public async Task<DuffelOfferResponse> AddServiceAsync(string offerId, string serviceId, int quantity = 1)
    {
        if (string.IsNullOrWhiteSpace(offerId))
            throw new ArgumentException("Offer id must be provided", nameof(offerId));

        // get available services for the offer
        var availableServices = await ListOfferAvailableServicesAsync(offerId);

        if (availableServices == null || !availableServices.Any())
        {
            _logger.LogWarning("No available services found for offer {OfferId}.", offerId);
            throw new InvalidOperationException("No available services found for the offer.");
        }

        var service = availableServices.Where(x => x.Id == serviceId).FirstOrDefault();

        if (service == null) throw new InvalidOperationException($"Service not found for the given offer: {offerId}");

        var request = new DuffelOfferPricingRequest
        {
            IntendedPaymentMethods = new List<PaymentMethod>
            {
                new PaymentMethod
                {
                    Type = "balance",
                    Currency = service.TotalCurrency,
                    Amount = service.TotalAmount
                }
            },
            IntendedServices = new List<IntendedService>
            {
                new IntendedService
                {
                    Id = service.Id,
                    Quantity = quantity
                }
            }
        };

        var pricedOffer = await _duffelService.PriceOfferAsync(offerId, request);

        return pricedOffer;
    }

    public async Task<CheckoutSessionResponse?> GeneratePaymentLinkAsync(PaymentLinkRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException("Invalid request: the payment request is null", nameof(request));

        var offer = await GetOfferAsync(request?.OfferId!);

        if (offer == null)
            throw new InvalidOperationException($"Invalid request: offer with id {request?.OfferId} not found.");

        // Retrieve Search
        var searchEntity = await _searchRepository.GetSearchByBookingOfferRequestIdAsync(request?.OfferRequestId!);

        // save the passenger details in the database

        foreach (var passenger in request?.Passengers!)
        {
            var passengerEntity = new Entities.Passenger()
            {
                Id = Guid.NewGuid(),
                PassengerId = passenger.Id,
                SearchId = searchEntity?.Id ?? Guid.Empty,
                BookingOfferId = request?.OfferId!,
                PassengerType = passenger.Type!,
                Title = passenger.Title,
                GivenName = passenger.GivenName,
                FamilyName = passenger.FamilyName,
                Gender = passenger.Gender,
                BornOn = passenger.BornOn,
                Email = passenger.Email,
                PhoneNumber = passenger.PhoneNumber,
                DocumentType = passenger.DocumentType,
                DocumentNumber = passenger.DocumentNumber,
                DocumentExpiryDate = passenger.DocumentExpiryDate,
                DocumentIssuingCountry = passenger.DocumentIssuingCountry,
            };
            await _passengerRepository.AddAsync(passengerEntity);
        }

        await _passengerRepository.SaveChangesAsync();

        string offerTitle = $"{offer?.Slices?.FirstOrDefault()?.Origin?.Name} ({offer?.Slices?.FirstOrDefault()?.Origin?.IataCityCode}) to {offer?.Slices?.FirstOrDefault()?.Destination?.Name} ({offer?.Slices?.FirstOrDefault()?.Destination?.IataCityCode}) | " + (offer?.Slices?.Count > 1 ? "Round-Trip" : "One-Way");
        string offerDescription =  $"Departure: {offer?.Slices?.FirstOrDefault()?.Segments?.FirstOrDefault()?.DepartingAt} - Arrival: {offer?.Slices?.FirstOrDefault()?.Segments?.FirstOrDefault()?.ArrivingAt}\n" +
            $"Departure Terminal: {offer?.Slices?.FirstOrDefault()?.Segments?.FirstOrDefault()?.OriginTerminal} - Arrival Terminal: {offer?.Slices?.FirstOrDefault()?.Segments?.FirstOrDefault()?.DestinationTerminal}";

        var checkoutSessionRequest = new CheckoutSessionRequest
        {
            IdempotencyKey = request?.OfferId!,
            LineItems = new List<LineItem>
            {
                new LineItem
                {
                    Quantity = 1,
                    PriceData = new PriceData
                    {
                        Currency = offer?.TotalCurrency?.ToLower(),
                        UnitAmount = (int)(decimal.Parse(offer?.TotalAmount?.ToString() ?? "0") * 100), // Stripe expects the amount in the smallest currency unit (e.g., cents for USD)
                        ProductData = new ProductData
                        {
                            Name = offerTitle,
                            Description = offerDescription,
                            Images = new List<string> { offer?.Owner?.LogoSymbolUrl! }
                        }
                    }
                }
            },
            Metadata = new Dictionary<string, string>
            {
                ["offer_id"] = offer?.Id ?? string.Empty,
            }
        };


        var checkoutSession = await _stripeService.CreateCheckoutSessionAsync(checkoutSessionRequest, cancellationToken);
        if (checkoutSession == null)
            _logger.LogWarning("CreateCheckoutSessionAsync returned null for request {@Request}", checkoutSessionRequest);


        return checkoutSession;
    }


    public async Task<DuffelPassengerResponse?> UpdatePassengerAsync(string offerId, string offerPassengerId, DuffelPassengerRequest request, CancellationToken cancellationToken = default)
    {
        var updatedPassenger = await _duffelService.UpdatePassengerAsync(offerId, offerPassengerId, request, cancellationToken);

        if (updatedPassenger == null)
            _logger.LogWarning("UpdatePassengerAsync returned null for offerId {OfferId} and passengerId {PassengerId}", offerId, offerPassengerId);


        return updatedPassenger;
    }

    public async Task<DuffelOrderResponse?> CreateOrderWithBalanceAsync(string offerId, CancellationToken cancellationToken)
    {

        var offer = await GetOfferAsync(offerId, true, cancellationToken);

        // get passengers from the database using offerId

        var passengers = await _passengerRepository.GetPassengersByBookingOfferIdAsync(offerId);


        var orderRequest = new DuffelOrderRequest
        {
            Type = "instant",
            SelectedOffers = new List<string> { offerId },
            Payments = new List<DuffelPayment>() {
                new DuffelPayment() {
                    Type = "balance",
                    Currency = offer?.TotalCurrency,
                    Amount = offer?.TotalAmount
                }
            },
            Passengers = passengers.Select(p => new DuffelOfferPassenger
            {
                Id = p.PassengerId,
                Title = p.Title,
                Email = p.Email,
                GivenName = p.GivenName,
                FamilyName = p.FamilyName,
                Gender = p.Gender,
                BornOn = p.BornOn,
                PhoneNumber = p.PhoneNumber
            }).ToList(),

            Metadata = new Dictionary<string, object> { { "source", "TravelerWay" }, { "offer_id", offerId } }
        };

        var order = await _duffelService.CreateOrderAsync(orderRequest, cancellationToken);

        var offerEntity = await _offerRepository.GetOfferByBookingOfferIdAsync(offerId);

        var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(offerEntity?.UserId?.ToString()!));

        await _notificationService.SendNotificationAsync<DuffelOrderResponse>(userEntity?.ChatId!,"Booking Confirmation", order!, cancellationToken);

        return order;

    }

}
