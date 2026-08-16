using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;
using TravelerWay.Common.Entities;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Payloads;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelerWay.Api.Controllers
{
    [Route("api/duffel/orders")]
    [ApiController]
    public class DuffelOrdersController : ControllerBase
    {
        private readonly IDuffelService _duffelService;
        private readonly ILogger<DuffelOrdersController> _logger;

        public DuffelOrdersController(IDuffelService duffelService, ILogger<DuffelOrdersController> logger)
        {
            _duffelService = duffelService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int? limit = null, [FromQuery] string? before = null, [FromQuery] string? after = null, [FromQuery(Name = "booking_reference")] string? bookingReference = null, [FromQuery] string? offerId = null, [FromQuery(Name = "awaiting_payment")] bool? awaitingPayment = null, [FromQuery] string? sort = null, [FromQuery(Name = "owner_ids")] IEnumerable<string>? ownerIds = null, [FromQuery(Name = "origin_ids")] IEnumerable<string>? originIds = null, [FromQuery(Name = "destination_ids")] IEnumerable<string>? destinationIds = null, [FromQuery(Name = "departing_at")] string? departingAt = null, [FromQuery(Name = "arriving_at")] string? arrivingAt = null, [FromQuery] string? createdAt = null, [FromQuery(Name = "passenger_names")] IEnumerable<string>? passengerNames = null, [FromQuery(Name = "requires_action")] bool? requiresAction = null, [FromQuery(Name = "user_id")] string? userId = null)
        {
            try
            {
                var orders = await _duffelService.ListOrdersAsync(limit, before, after, bookingReference, offerId, awaitingPayment, sort, ownerIds, originIds, destinationIds, departingAt, arrivingAt, createdAt, passengerNames, requiresAction, userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error listing Duffel orders");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            try
            {
                var order = await _duffelService.GetOrderAsync(id, CancellationToken.None);

                if (order == null) return NotFound();

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving Duffel order {id}");
                return StatusCode(500, $"Internal server error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpGet("{id}/available_services")]
        public async Task<IActionResult> ListOrderAvailableServices([FromRoute] string id)
        {
            try
            {
                var availableServices = await _duffelService.ListOrderAvailableServicesAsync(id, CancellationToken.None);
                if (availableServices == null) return NotFound();

                return Ok(availableServices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving Duffel order {id}");
                return StatusCode(500, $"Internal server error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
        [HttpPost("{orderId}/services")]
        public async Task<IActionResult> AddServices([FromRoute] string orderId, [FromBody] DuffelOrderServiceAdditionRequest additionRequest)
        {
            try
            {
                var createdOrder = await _duffelService.AddServicesToOrderAsync(orderId, additionRequest, CancellationToken.None);

                return Ok(createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding services to Duffel order {orderId}");
                return StatusCode(500, $"Internal server error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DuffelOrderRequest order, CancellationToken cancellationToken = default)
        {
            try
            {
                var createdOrder = await _duffelService.CreateOrderAsync(order, cancellationToken);

                return Ok(createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating Duffel order");
                return StatusCode(500, $"Internal server error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpPatch("{orderId}")]
        public async Task<IActionResult> Put([FromRoute] string orderId, [FromBody] DuffelUpdateOrderRequest orderUpdate, CancellationToken cancellationToken = default)
        {
            try
            {
                var createdOrder = await _duffelService.UpdateOrderAsync(orderId, orderUpdate, cancellationToken);

                return Ok(createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating Duffel order {orderId}");
                return StatusCode(500, $"Internal server error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}
