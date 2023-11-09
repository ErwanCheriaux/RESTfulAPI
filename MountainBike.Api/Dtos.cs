using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MountainBike.Api;

public record BikeDto(
    Guid Id,
    string? Brand,
    string? Model,
    int Year,
    string? Material,
    string? Color,
    string? Size,
    string? SerialNumber,
    DateTimeOffset CreationDate
);

public record CreateBikeDto(
    [Required] string? Brand,
    [Required] string? Model,
    [Range(1900, 2900)] int Year,
    string? Material,
    string? Color,
    string? Size,
    string? SerialNumber
);

public record UpdateBikeDto(
    [Required] string? Brand,
    [Required] string? Model,
    [Range(1900, 2900)] int Year,
    string? Material,
    string? Color,
    string? Size,
    string? SerialNumber
);

public record RiderDto(
    Guid Id,
    string? Name,
    int Age,
    string? Country,
    DateTimeOffset CreationDate
);

public record CreateRiderDto(
    [Required] string? Name,
    [Range(4, 120)] int Age,
    string? Country
);

public record UpdateRiderDto(
    [Required] string? Name,
    [Range(4, 120)] int Age,
    string? Country
);