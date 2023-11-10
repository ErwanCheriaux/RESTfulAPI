using Microsoft.AspNetCore.Mvc;
using MountainBike.Api.DataAccess;
using MountainBike.Api.Models;

namespace MountainBike.Api.Controllers;

[ApiController]
[Route("riders")]
public class RiderController : ControllerBase
{
    private readonly IGarage _garage;
    private readonly ILogger<RiderController> _logger;

    public RiderController(IGarage garage, ILogger<RiderController> logger)
    {
        _garage = garage;
        _logger = logger;
    }

    // GET /riders
    [HttpGet]
    public async Task<IEnumerable<RiderDto>> GetRidersAsync(string? Name = null)
    {
        var riders = (await _garage.GetRidersAsync()).Select(riders => riders.AsDto());

        if (!string.IsNullOrWhiteSpace(Name))
        {
            riders = riders.Where(rider => rider.Name!.Contains(Name, StringComparison.OrdinalIgnoreCase));
        }

        _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {riders.Count()} rider(s)");

        return riders;
    }

    // GET /riders/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RiderDto>> GetRiderAsync(Guid id)
    {
        var rider = await _garage.GetRiderAsync(id);

        if (rider is null)
        {
            return NotFound();
        }

        return rider.AsDto();
    }

    // POST /riders
    [HttpPost]
    public async Task<ActionResult<RiderDto>> CreateRiderAsync(CreateRiderDto riderDto)
    {
        Rider rider = new()
        {
            Id = Guid.NewGuid(),
            Name = riderDto.Name,
            Birthdate = riderDto.Birthdate,
            Country = riderDto.Country,
            CreationDate = DateTimeOffset.UtcNow
        };

        await _garage.CreateRiderAsync(rider);

        return CreatedAtAction(nameof(GetRiderAsync), new { Id = rider.Id }, rider.AsDto());
    }

    // PUT /riders/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRiderAsync(Guid id, UpdateRiderDto riderDto)
    {
        var existingRider = await _garage.GetRiderAsync(id);

        if (existingRider is null)
        {
            return NotFound();
        }

        existingRider.Name = riderDto.Name;
        existingRider.Birthdate = riderDto.Birthdate;
        existingRider.Country = riderDto.Country;

        await _garage.UpdateRiderAsync(existingRider);

        return NoContent();
    }

    // DELETE /riders/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRiderAsync(Guid id)
    {
        var existingRider = await _garage.GetRiderAsync(id);

        if (existingRider is null)
        {
            return NotFound();
        }

        await _garage.DeleteRiderAsync(id);

        return NoContent();
    }
}
