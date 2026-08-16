using Microsoft.AspNetCore.Mvc;
using TravelerWay.Common.Entities;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Payloads;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelerWay.Api.Controllers
{
    [ApiController]
    [Route("api/duffel/offer-requests")]
    public class DuffelOfferRequestsController : ControllerBase
    {
        private readonly IDuffelService _duffelService;
        private readonly ILogger<DuffelOfferRequestsController> _logger;

        public DuffelOfferRequestsController(IDuffelService duffelService, ILogger<DuffelOfferRequestsController> logger)
        {
            _duffelService = duffelService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<DuffelResponseWithMetaData<IEnumerable<DuffelOfferReqResponse>, DuffelPaginationFilters>>> List([FromQuery] int? limit = 50, [FromQuery] string? before = null, [FromQuery] string? after = null, [FromQuery] string? sort = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var offers = await _duffelService.ListOfferRequestsAsync(limit, before, after, sort, cancellationToken);

                if(offers == null)
                {
                    return NotFound();
                }

                return Ok(offers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Listing Duffel offer requests");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DuffelOfferReqResponse>> Get(string id)
        {
            try
            {
                var offer = await _duffelService.GetOfferRequestAsync(id);
                if (offer == null)
                {
                    return NotFound();
                }
                return Ok(offer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Duffel offer request");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DuffelOfferReqRequest offerRequest, [FromQuery] bool returnOffers = false, CancellationToken cancellationToken = default)
        {
            if (offerRequest == null)
                return BadRequest();

            try
            {
                var created = await _duffelService.CreateOfferRequestAsync(offerRequest, returnOffers, cancellationToken);
                if (created == null)
                    return StatusCode(502, "Failed to create offer request at Duffel");

                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Duffel offer request");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
