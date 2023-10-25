using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.DataAccess;
using RESTfulAPI.DataTransferObjects;

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
}
