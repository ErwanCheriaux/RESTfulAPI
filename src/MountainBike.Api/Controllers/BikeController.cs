using Microsoft.AspNetCore.Mvc;
using MountainBike.Services.Entities;
using MountainBike.Services.Services;

namespace MountainBike.Api.Controllers;

[ApiController]
[Route("bikes")]
public class BikeController : ControllerBase
{
    private readonly IBikeService _bikeService;
    private readonly ILogger<BikeController> _logger;

    public BikeController(IBikeService bikeService, ILogger<BikeController> logger)
    {
        _bikeService = bikeService;
        _logger = logger;
    }

    // GET /bikes
    [HttpGet]
    public async Task<IEnumerable<BikeDto>> GetBikesAsync(string? Brand = null)
    {
        var bikes = (await _bikeService.GetBikesAsync()).Select(bikes => bikes.AsDto());

        if (!string.IsNullOrWhiteSpace(Brand))
        {
            bikes = bikes.Where(bike => bike.Brand!.Contains(Brand, StringComparison.OrdinalIgnoreCase));
        }

        _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {bikes.Count()} bike(s)");

        return bikes;
    }

    // GET /bikes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<BikeDto>> GetBikeAsync(Guid id)
    {
        var bike = await _bikeService.GetBikeAsync(id);

        if (bike is null)
        {
            return NotFound();
        }

        return bike.AsDto();
    }

    // POST /bikes
    [HttpPost]
    public async Task<ActionResult<BikeDto>> CreateBikeAsync(CreateBikeDto bikeDto)
    {
        BikeEntity bike = new()
        {
            Id = Guid.NewGuid(),
            Brand = bikeDto.Brand,
            Model = bikeDto.Model,
            Year = bikeDto.Year,
            Material = bikeDto.Material,
            Color = bikeDto.Color,
            Size = bikeDto.Size,
            SerialNumber = bikeDto.SerialNumber,
            CreationDate = DateTimeOffset.UtcNow
        };

        await _bikeService.CreateBikeAsync(bike);

        return CreatedAtAction(nameof(GetBikeAsync), new { Id = bike.Id }, bike.AsDto());
    }

    // PUT /bikes/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBikeAsync(Guid id, UpdateBikeDto bikeDto)
    {
        var existingBike = await _bikeService.GetBikeAsync(id);

        if (existingBike is null)
        {
            return NotFound();
        }

        existingBike.Brand = bikeDto.Brand;
        existingBike.Model = bikeDto.Model;
        existingBike.Year = bikeDto.Year;
        existingBike.Material = bikeDto.Material;
        existingBike.Color = bikeDto.Color;
        existingBike.Size = bikeDto.Size;
        existingBike.SerialNumber = bikeDto.SerialNumber;

        await _bikeService.UpdateBikeAsync(existingBike);

        return NoContent();
    }

    // DELETE /bikes/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBikeAsync(Guid id)
    {
        var existingBike = await _bikeService.GetBikeAsync(id);

        if (existingBike is null)
        {
            return NotFound();
        }

        await _bikeService.DeleteBikeAsync(id);

        return NoContent();
    }
}
