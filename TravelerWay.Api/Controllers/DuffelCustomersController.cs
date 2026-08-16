using Microsoft.AspNetCore.Mvc;
using TravelerWay.Common.Interfaces;
using TravelerWay.Common.Payloads;

namespace TravelerWay.Api.Controllers;

[ApiController]
[Route("api/duffel/customers")]
public class DuffelCustomersController : ControllerBase
{
    private readonly IDuffelService _duffelService;
    private readonly ILogger<DuffelCustomersController> _logger;

    public DuffelCustomersController(IDuffelService duffelService, ILogger<DuffelCustomersController> logger)
    {
        _duffelService = duffelService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DuffelCustomerRequest customer, CancellationToken cancellationToken = default)
    {
        if (customer == null)
            return BadRequest();

        try
        {
            var created = await _duffelService.CreateCustomerAsync(customer, cancellationToken);
            if (created == null)
                return StatusCode(502, "Failed to create customer at Duffel");

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Duffel customer");
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpGet]
    public async Task<IActionResult> List(string? email, int? limit = null, string? before = null, string? after = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var duffelCustomers = await _duffelService.ListCustomersAsync(email, limit, before, after, cancellationToken);
            return Ok(duffelCustomers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing Duffel customers");
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var duffelCustomers = await _duffelService.GetCustomerAsync(id);
            return Ok(duffelCustomers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing Duffel customers");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] DuffelCustomerRequest customerUpdate)
    {
        try
        {
            var updated = await _duffelService.UpdateCustomerAsync(id, customerUpdate);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating Duffel customer");
            return StatusCode(500, "Internal server error");
        }
    }
}
