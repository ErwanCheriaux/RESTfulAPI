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
    public IEnumerable<BikeDto> GetBikes()
    {
        var bikes = _garage.GetBikes().Select(bikes => bikes.AsDto());
        return bikes;
    }

    // GET /bikes/{id}
    [HttpGet("{id}")]
    public ActionResult<BikeDto> GetBike(Guid id)
    {
        var bike = _garage.GetBike(id);

        if (bike is null)
        {
            return NotFound();
        }

        return bike.AsDto();
    }

    // POST /bikes
    [HttpPost]
    public ActionResult<BikeDto> CreateBike(CreateBikeDto bikeDto)
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
            SerialNumber = bikeDto.SerialNumber
        };

        _garage.CreateBike(bike);

        return CreatedAtAction(nameof(GetBike), new { Id = bike.Id }, bike.AsDto());
    }

    // PUT /bikes/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateBike(Guid id, UpdateBikeDto bikeDto)
    {
        var existingBike = _garage.GetBike(id);

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

        _garage.UpdateBike(updatedBike);

        return NoContent();
    }

    // DELETE /bikes/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteBike(Guid id)
    {
        var existingBike = _garage.GetBike(id);

        if (existingBike is null)
        {
            return NotFound();
        }

        _garage.DeleteBike(id);

        return NoContent();
    }
}
