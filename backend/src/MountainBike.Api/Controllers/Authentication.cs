using Microsoft.AspNetCore.Mvc;

namespace MountainBike.Api.Controllers;

[ApiController]
public class Authentication : ControllerBase
{
    private readonly ILogger<Authentication> _logger;

    public Authentication(ILogger<Authentication> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Route("signup")]
    public ActionResult AuthSignup(Object credentials)
    {
        return NoContent();
    }

    [HttpPost]
    [Route("login")]
    public ActionResult AuthLogin(Object credentials)
    {
        return NoContent();
    }
}