using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Payloads;

namespace TravelerWay.Api.Controllers
{
    [Route("api/duffel/order-changes")]
    [ApiController]
    public class DuffelOrderChangesController : ControllerBase
    {
        private readonly IDuffelService _duffelService;
        private readonly ILogger<DuffelOrderChangesController> _logger;

        public DuffelOrderChangesController(IDuffelService duffelService, ILogger<DuffelOrderChangesController> logger)
        {
            _duffelService = duffelService;
            _logger = logger;
        }


        [HttpPost]
        public async Task<ActionResult<DuffelOrderChange?>> Create([FromBody] DuffelOrderChangeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.SelectedOrderChangeOfferId))
                    return BadRequest();

                var created = await _duffelService.CreateOrderChangeAsync(request.SelectedOrderChangeOfferId, cancellationToken);
                if (created == null) return NotFound();

                return Ok(created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Duffel order change request {Id}", request.SelectedOrderChangeOfferId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DuffelOrderChange?>> Get(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id)) return BadRequest();

                var orderChange = await _duffelService.GetOrderChangeAsync(id, cancellationToken);
                if (orderChange == null) return NotFound();

                return Ok(orderChange);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Duffel order change request {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the request.");
            }
        }

        [HttpPost("{id}/actions/confirm")]
        public async Task<ActionResult<DuffelOrderChange?>> Confirm(string id, [FromBody] DuffelPayment payment, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || payment == null) return BadRequest();

                var confirmed = await _duffelService.ConfirmOrderChangeAsync(id, payment, cancellationToken);
                if (confirmed == null) return NotFound();

                return Ok(confirmed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Duffel order change request {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the request.");
            }
        }
    }
}
