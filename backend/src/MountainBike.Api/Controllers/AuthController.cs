using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MountainBike.Services.Entities;
using MountainBike.Services.Services;

namespace MountainBike.Api.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUserService userService,
                          IConfiguration configuration,
                          ILogger<AuthController> logger)
    {
        _userService = userService;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost]
    [Route("signup")]
    public async Task<ActionResult<string>> AuthSignup(UserDto request)
    {
        if (await _userService.EmailExistAsync(request.Email))
        {
            return Conflict();
        }

        UserEntity user = new()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreationDate = DateTimeOffset.UtcNow
        };

        await _userService.CreateUserAsync(user);
        string token = CreateToken(request.Email);
        var response = JsonSerializer.Serialize(new { token });

        return Ok(response);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<string>> AuthLogin(UserDto request)
    {
        var user = await _userService.GetUserAsync(request.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid credentials");
        }

        string token = CreateToken(request.Email);
        var response = JsonSerializer.Serialize(new { token });

        return Ok(response);
    }

    private string CreateToken(string email)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("JwtSettings:Key").Value!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: _configuration.GetSection("JwtSettings:Issuer").Value,
            audience: _configuration.GetSection("JwtSettings:Audience").Value,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}