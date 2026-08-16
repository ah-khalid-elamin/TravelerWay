using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Payloads; // adjust if your service lives in a different namespace

namespace TravelerWay.Api.Controllers
{
    [Route("api/duffel/order-cancellations")]
    [ApiController]
    public class DuffelOrderCancellationsController : ControllerBase
    {
        private readonly IDuffelService _duffelService;
        private readonly ILogger<DuffelOrderCancellationsController> _logger;

        public DuffelOrderCancellationsController(IDuffelService duffelService, ILogger<DuffelOrderCancellationsController> logger)
        {
            _duffelService = duffelService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string? orderId = null, [FromQuery] int? limit = null, [FromQuery] string? before = null, [FromQuery] string? after = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var list = await _duffelService.ListOrderCancellationsAsync(orderId, limit, before, after, cancellationToken);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await _duffelService.GetOrderCancellationAsync(id, cancellationToken);
                if (item == null) return NotFound();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DuffelOrderCancellationRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var created = await _duffelService.CreateOrderCancellationAsync(request, cancellationToken);
                if (created == null) return BadRequest("Unable to create order cancellation.");
                return Ok(created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id}/actions/confirm")]
        public async Task<IActionResult> Confirm([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var confirmed = await _duffelService.ConfirmOrderCancellationAsync(id, cancellationToken);
                if (confirmed == null) return NotFound();
                return Ok(confirmed);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
