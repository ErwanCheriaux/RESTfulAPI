using Microsoft.AspNetCore.Mvc;
using MountainBike.Services.Entities;
using MountainBike.Services.Services;

namespace MountainBike.Api.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUserService userService, ILogger<AuthController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost]
    [Route("signup")]
    public async Task<ActionResult> AuthSignup(UserDto request)
    {
        if (await _userService.EmailExistAsync(request.Email))
        {
            return Conflict();
        }

        UserEntity user = new()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Passwordhash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreationDate = DateTimeOffset.UtcNow
        };

        await _userService.CreateUserAsync(user);
        return NoContent();
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> AuthLogin(UserDto request)
    {
        return NoContent();
    }
}