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
    public async Task<ActionResult<RiderDetailsDto>> GetRiderAsync(Guid id)
    {
        var rider = await _garage.GetRiderAsync(id);

        if (rider is null)
        {
            return NotFound();
        }

        var riderbikes = (await _garage.GetBikesAsync()).Where(bike => bike.RiderId == id);

        return rider.AsDetailsDto(riderbikes.Count());
    }

    // POST /riders
    [HttpPost]
    public async Task<ActionResult<RiderDetailsDto>> CreateRiderAsync(CreateRiderDto riderDto)
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

        return CreatedAtAction(nameof(GetRiderAsync), new { Id = rider.Id }, rider.AsDetailsDto(bikeCount: 0));
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

    // GET /riders/{rider_id}/bikes
    [HttpGet("{rider_id}/bikes")]
    public async Task<IEnumerable<BikeDto>> GetRiderBikesAsync(Guid rider_id)
    {
        var riderbikes = (await _garage.GetBikesAsync()).Where(bike => bike.RiderId == rider_id);
        return riderbikes.Select(bike => bike.AsDto());
    }

    // PUT /riders/{rider_id}/bikes/{bike_id}
    [HttpPut("{rider_id}/bikes/{bike_id}")]
    public async Task<ActionResult> UpdateRiderBikeAsync(Guid rider_id, Guid bike_id)
    {
        var rider = await _garage.GetRiderAsync(rider_id);
        var bike = await _garage.GetBikeAsync(bike_id);

        if (rider is null || bike is null)
        {
            return NotFound();
        }

        bike.RiderId = rider_id;

        await _garage.UpdateBikeAsync(bike);

        return NoContent();
    }

    // DELETE /riders/{rider_id}/bikes/{bike_id}
    [HttpDelete("{rider_id}/bikes/{bike_id}")]
    public async Task<ActionResult> DeleteRiderBikeAsync(Guid rider_id, Guid bike_id)
    {
        var rider = await _garage.GetRiderAsync(rider_id);
        var bike = await _garage.GetBikeAsync(bike_id);

        if (rider is null || bike is null)
        {
            return NotFound();
        }

        bike.RiderId = null;

        await _garage.UpdateBikeAsync(bike);

        return NoContent();
    }
}
