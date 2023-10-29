using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.DataAccess;
using RESTfulAPI.DataTransferObjects;
using RESTfulAPI.Models;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("bikes")]
public class BikeController : ControllerBase
{
    private readonly IGarage _garage;

    public BikeController(IGarage garage)
    {
        _garage = garage;
    }

    // GET /bikes
    [HttpGet]
    public async Task<IEnumerable<BikeDto>> GetBikesAsync()
    {
        var bikes = (await _garage.GetBikesAsync()).Select(bikes => bikes.AsDto());
        return bikes;
    }

    // GET /bikes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<BikeDto>> GetBikeAsync(Guid id)
    {
        var bike = await _garage.GetBikeAsync(id);

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
        Bike bike = new()
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

        await _garage.CreateBikeAsync(bike);

        return CreatedAtAction(nameof(GetBikeAsync), new { Id = bike.Id }, bike.AsDto());
    }

    // PUT /bikes/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBikeAsync(Guid id, UpdateBikeDto bikeDto)
    {
        var existingBike = await _garage.GetBikeAsync(id);

        if (existingBike is null)
        {
            return NotFound();
        }

        Bike updatedBike = existingBike with
        {
            Brand = bikeDto.Brand,
            Model = bikeDto.Model,
            Year = bikeDto.Year,
            Material = bikeDto.Material,
            Color = bikeDto.Color,
            Size = bikeDto.Size,
            SerialNumber = bikeDto.SerialNumber
        };

        await _garage.UpdateBikeAsync(updatedBike);

        return NoContent();
    }

    // DELETE /bikes/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBikeAsync(Guid id)
    {
        var existingBike = await _garage.GetBikeAsync(id);

        if (existingBike is null)
        {
            return NotFound();
        }

        await _garage.DeleteBikeAsync(id);

        return NoContent();
    }
}
