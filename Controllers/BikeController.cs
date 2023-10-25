using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.DataAccess;
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
    public IEnumerable<Bike> GetBikes()
    {
        var bikes = _garage.GetBikes();
        return bikes;
    }

    // GET /bikes/{id}
    [HttpGet("{id}")]
    public ActionResult<Bike> GetBike(Guid id)
    {
        var bike = _garage.GetBike(id);

        if (bike is null)
        {
            return NotFound();
        }

        return bike;
    }
}
