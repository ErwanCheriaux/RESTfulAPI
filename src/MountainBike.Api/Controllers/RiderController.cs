using Microsoft.AspNetCore.Mvc;
using MountainBike.Services.Entities;
using MountainBike.Services.Services;

namespace MountainBike.Api.Controllers;

[ApiController]
[Route("riders")]
public class RiderController : ControllerBase
{
    private readonly IRiderService _riderService;
    private readonly IBikeService _bikeService;
    private readonly ILogger<RiderController> _logger;

    public RiderController(IRiderService riderService, IBikeService bikeService, ILogger<RiderController> logger)
    {
        _riderService = riderService;
        _bikeService = bikeService;
        _logger = logger;
    }

    // GET /riders
    [HttpGet]
    public async Task<IEnumerable<RiderDto>> GetRidersAsync(string? Name = null)
    {
        var riders = (await _riderService.GetRidersAsync()).Select(riders => riders.AsDto());

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
        var rider = await _riderService.GetRiderAsync(id);

        if (rider is null)
        {
            return NotFound();
        }

        var riderbikes = await _bikeService.GetBikesByRiderIdAsync(id);

        return rider.AsDetailsDto(riderbikes.Count());
    }

    // POST /riders
    [HttpPost]
    public async Task<ActionResult<RiderDetailsDto>> CreateRiderAsync(CreateRiderDto riderDto)
    {
        RiderEntity rider = new()
        {
            Id = Guid.NewGuid(),
            Name = riderDto.Name,
            Birthdate = riderDto.Birthdate,
            Country = riderDto.Country,
            CreationDate = DateTimeOffset.UtcNow
        };

        await _riderService.CreateRiderAsync(rider);

        return CreatedAtAction(nameof(GetRiderAsync), new { Id = rider.Id }, rider.AsDetailsDto(bikeCount: 0));
    }

    // PUT /riders/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRiderAsync(Guid id, UpdateRiderDto riderDto)
    {
        var existingRider = await _riderService.GetRiderAsync(id);

        if (existingRider is null)
        {
            return NotFound();
        }

        existingRider.Name = riderDto.Name;
        existingRider.Birthdate = riderDto.Birthdate;
        existingRider.Country = riderDto.Country;

        await _riderService.UpdateRiderAsync(existingRider);

        return NoContent();
    }

    // DELETE /riders/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRiderAsync(Guid id)
    {
        var existingRider = await _riderService.GetRiderAsync(id);

        if (existingRider is null)
        {
            return NotFound();
        }

        await _riderService.DeleteRiderAsync(id);

        return NoContent();
    }

    // GET /riders/{rider_id}/bikes
    [HttpGet("{rider_id}/bikes")]
    public async Task<IEnumerable<BikeDto>> GetRiderBikesAsync(Guid rider_id)
    {
        var riderbikes = await _bikeService.GetBikesByRiderIdAsync(rider_id);
        return riderbikes.Select(bike => bike.AsDto());
    }

    //
    // Summary:
    //     Map a rider and a bike in the repository.
    //
    // Parameters:
    //     rider_id: 
    //         Rider collecting the bike_id
    //
    //     bike_id:
    //         Bike received by rider_id
    //
    // Endpoint:
    //     PUT /riders/{rider_id}/bikes/{bike_id}
    //
    [HttpPut("{rider_id}/bikes/{bike_id}")]
    public async Task<ActionResult> AddRiderBikeAsync(Guid rider_id, Guid bike_id)
    {
        var rider = await _riderService.GetRiderAsync(rider_id);
        var bike = await _bikeService.GetBikeAsync(bike_id);

        if (rider is null || bike is null)
        {
            return NotFound();
        }

        bike.RiderId = rider_id;

        await _bikeService.UpdateBikeAsync(bike);

        return NoContent();
    }

    //
    // Summary:
    //     Unmap a rider and a bike in the repository.
    //
    // Parameters:
    //     rider_id: 
    //         Rider loosing the bike_id
    //
    //     bike_id:
    //         Bike removed from rider_id
    //
    // Endpoint:
    //     DELETE /riders/{rider_id}/bikes/{bike_id}
    //
    [HttpDelete("{rider_id}/bikes/{bike_id}")]
    public async Task<ActionResult> RemoveRiderBikeAsync(Guid rider_id, Guid bike_id)
    {
        var rider = await _riderService.GetRiderAsync(rider_id);
        var bike = await _bikeService.GetBikeAsync(bike_id);

        if (rider is null || bike is null)
        {
            return NotFound();
        }

        bike.RiderId = null;

        await _bikeService.UpdateBikeAsync(bike);

        return NoContent();
    }
}
