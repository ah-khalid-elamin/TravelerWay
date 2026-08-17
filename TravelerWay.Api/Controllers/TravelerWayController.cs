using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Interfaces.Implementations;
using TravelerWay.Common.Payloads;

namespace TravelerWay.Api.Controllers
{
    [Route("api/travelerway/flight-booking")]
    [ApiController]
    public class TravelerWayController : ControllerBase
    {
        private readonly ITravelerWayService _travelerWayService;
        private readonly ILogger<TravelerWayController> _logger;

        public TravelerWayController(ITravelerWayService travelerWayService, ILogger<TravelerWayController> logger)
        {
            _travelerWayService = travelerWayService;
            _logger = logger;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(
            [FromBody] DuffelOfferReqRequest offerReqRequest,
            [FromQuery(Name = "return_offers")] bool returnOffers = false,
            [FromQuery] int? limit = null,
            [FromQuery] string? sort = null,
            [FromQuery(Name = "require_instant_payment")] bool? requiresInstantPayment = null)
        {
            var response = await _travelerWayService.SearchOffers(
                offerReqRequest,
                returnOffers,
                limit,
                sort,
                requiresInstantPayment);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("offers")]
        public async Task<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferResponse>>> ListOffersAsync(
            [FromQuery(Name = "offer_request_id")] string offerRequestId,
            [FromQuery] int? limit = null,
            [FromQuery(Name = "max_connections")] int? maxConnections = null,
            [FromQuery] string? sort = null,
            [FromQuery(Name = "require_instant_payments")] bool? requiresInstantPayment = null)
        {
            var offers = await _travelerWayService.ListOffersAsync(offerRequestId, limit, maxConnections, sort, requiresInstantPayment);
            return offers;
        }

        [HttpGet("offers/{offerId}")]
        public async Task<IActionResult> GetOfferAsync(
            [FromRoute] string offerId,
            [FromQuery(Name = "return_available_services")] bool? returnAvailableServices = null)
        {
            if (string.IsNullOrWhiteSpace(offerId))
            {
                return BadRequest("offerId is required.");
            }

            var offer = await _travelerWayService.GetOfferAsync(offerId, returnAvailableServices);
            if (offer == null)
            {
                _logger.LogInformation("Offer not found: {OfferId}", offerId);
                return NotFound();
            }

            return Ok(offer);
        }

        [HttpGet("offers/{offerId}/available-services")]
        public async Task<IActionResult> ListOfferAvailableServicesAsync(
            [FromRoute] string offerId,
            [FromQuery(Name = "return_available_services")] bool? returnAvailableServices = true)
        {
            if (string.IsNullOrWhiteSpace(offerId))
            {
                return BadRequest("offerId is required.");
            }

            var services = await _travelerWayService.ListOfferAvailableServicesAsync(offerId, returnAvailableServices);
            if (services == null)
            {
                _logger.LogInformation("No available services found for offer: {OfferId}", offerId);
                return NotFound();
            }

            return Ok(services);
        }

        [HttpPost("offers/{offerId}/services/{serviceId}")]
        public async Task<IActionResult> AddServiceAsync(
            [FromRoute] string offerId,
            [FromRoute] string serviceId,
            [FromQuery] int quantity = 1)
        {
            if (string.IsNullOrWhiteSpace(offerId) || string.IsNullOrWhiteSpace(serviceId))
            {
                return BadRequest("offerId and serviceId are required.");
            }

            if (quantity <= 0)
            {
                return BadRequest("quantity must be greater than zero.");
            }


            var updatedOffer = await _travelerWayService.AddServiceAsync(offerId, serviceId, quantity);
            if (updatedOffer == null)
            {
                _logger.LogInformation("Failed to add service. Offer or service not found. OfferId: {OfferId}, ServiceId: {ServiceId}", offerId, serviceId);
                return NotFound();
            }

            return Ok(updatedOffer);

        }

        [HttpPost("payment-link")]
        public async Task<IActionResult> GeneratePaymentLinkAsync(
            [FromBody] PaymentLinkRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request?.OfferId))
            {
                return BadRequest("offerId is required.");
            }


            var session = await _travelerWayService.GeneratePaymentLinkAsync(request, cancellationToken);
            if (session == null)
            {
                _logger.LogInformation("Payment session not created for offer: {OfferId}", request?.OfferId);
                return NotFound();
            }

            return Ok(session.Url);

        }

        [HttpPost("{offerId}/passengers/{passengerId}")]
        public async Task<ActionResult<DuffelPassengerResponse>> UpdatePassenger([FromRoute] string offerId, [FromRoute] string passengerId, [FromBody] DuffelPassengerRequest request, CancellationToken cancellationToken = default)
        {

            var passenger = await _travelerWayService.UpdatePassengerAsync(offerId, passengerId, request, cancellationToken);

            if (passenger == null) return NotFound();

            return Ok(passenger);

        }

            //TODO: create order based on stripe webook payment
            // post-order requests: update order passengers details, add services to order

        }
}
