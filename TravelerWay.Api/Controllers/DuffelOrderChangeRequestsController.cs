using Microsoft.AspNetCore.Mvc;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Payloads;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelerWay.Api.Controllers
{
    [Route("api/duffel/order-change-requests")]
    [ApiController]
    public class DuffelOrderChangeRequestsController : ControllerBase
    {
        private readonly IDuffelService _duffelService;
        private readonly ILogger<DuffelOrderChangeRequestsController> _logger;

        public DuffelOrderChangeRequestsController(
            IDuffelService duffelService,
            ILogger<DuffelOrderChangeRequestsController> logger)
        {
            _duffelService = duffelService ?? throw new ArgumentNullException(nameof(duffelService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DuffelOrderChangeReqResponse?>> Get(string id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _duffelService.GetOrderChangeRequestAsync(id, cancellationToken);
                if (response is null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Duffel order change request {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the request.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DuffelOrderChangeReqResponse?>> Post([FromBody] DuffelOrderChangeReqRequest orderChangeRequest, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _duffelService.CreateOrderChangeRequestAsync(orderChangeRequest, cancellationToken);
                if (response is null) return BadRequest();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Duffel order change request");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the request.");
            }
        }
    }
}
