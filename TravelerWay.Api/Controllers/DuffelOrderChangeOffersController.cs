using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Payloads;   // adjust namespace if needed

namespace TravelerWay.Api.Controllers
{
    [Route("api/duffel/order-change-offers")]
    [ApiController]
    public class DuffelOrderChangeOffersController : ControllerBase
    {
        private readonly IDuffelService _duffelService;
        private readonly ILogger<DuffelOrderChangeOffersController> _logger;


        public DuffelOrderChangeOffersController(IDuffelService duffelService, ILogger<DuffelOrderChangeOffersController> logger)
        {
            _duffelService = duffelService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery(Name = "order_change_request_id")] string orderChangeRequestId,
            [FromQuery] int? limit = null,
            [FromQuery] string? before = null,
            [FromQuery] string? after = null,
            [FromQuery] string? sort = null,
            [FromQuery(Name = "max_connections")] int? maxConnections = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(orderChangeRequestId))
                {
                    return BadRequest("orderChangeRequestId is required.");
                }

                 var response = await _duffelService.ListOrderChangeOffersAsync(
                    orderChangeRequestId,
                    limit,
                    before,
                    after,
                    sort,
                    maxConnections,
                    cancellationToken);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Duffel order change request {Id}", orderChangeRequestId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest("id is required.");
                }

                DuffelOrderChangeOffer? offer = await _duffelService.GetOrderChangeOfferAsync(id, cancellationToken);

                if (offer is null)
                {
                    return NotFound();
                }

                return Ok(offer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Duffel order change request {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the request.");
            }
        }
    }
}
