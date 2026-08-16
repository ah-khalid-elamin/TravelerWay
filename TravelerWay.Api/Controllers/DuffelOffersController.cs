using Microsoft.AspNetCore.Mvc;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Payloads;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelerWay.Api.Controllers
{
    [Route("api/duffel/offers")]
    [ApiController]
    public class DuffelOffersController : ControllerBase
    {
        private readonly IDuffelService _duffelService;
        private readonly ILogger<DuffelCustomersController> _logger;

        public DuffelOffersController(IDuffelService duffelService, ILogger<DuffelCustomersController> logger)
        {
            _duffelService = duffelService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<DuffelResponseWithMetaData<DuffelPaginationFilters, IEnumerable<DuffelOfferResponse>>>> List(
            [FromQuery(Name = "offer_request_id")] string offerRequestId,
            [FromQuery] int? limit = 50,
            [FromQuery(Name = "max_connections")] int? maxConnections = null,
            [FromQuery] string? sort = null,
            [FromQuery(Name = "return_available_services")] bool? returnAvailableServices = null,
            [FromQuery(Name = "requires_instant_payment")] bool? requiresInstantPayment = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var offers = await _duffelService.ListOffersAsync(offerRequestId, limit, maxConnections, sort, requiresInstantPayment, cancellationToken);
                return Ok(offers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Duffel customer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DuffelOfferResponse>> Get([FromRoute] string id, [FromQuery] bool? returnAvailableServices = true)
        {
            try
            {
                var offer = await _duffelService.GetOfferAsync(id, returnAvailableServices);

                if (offer == null) return NotFound();

                return Ok(offer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Duffel customer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{offerId}/actions/price")]
        public async Task<ActionResult<DuffelOfferResponse>> Post([FromRoute] string offerId, [FromBody] DuffelOfferPricingRequest request)
        {
            try
            {
                var offer = await _duffelService.PriceOfferAsync(offerId, request);

                if (offer == null) return NotFound();

                return Ok(offer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Duffel customer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{offerId}/passengers/{passengerId}")]
        public async Task<ActionResult<DuffelPassengerResponse>> Patch([FromRoute] string offerId, [FromRoute] string passengerId, [FromBody] DuffelPassengerRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var passenger = await _duffelService.UpdatePassengerAsync(offerId, passengerId, request, cancellationToken);

                if (passenger == null) return NotFound();

                return Ok(passenger);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Duffel passenger");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
