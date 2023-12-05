using System.ComponentModel.DataAnnotations;
using MountainBike.Api.Attributes;

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
    DateOnly Birthdate,
    string? Country,
    DateTimeOffset CreationDate
);

public record CreateRiderDto()
{
    [Required] public string? Name { get; init; }
    [DateOnlyRange("1900-01-01", "2200-01-01")] public DateOnly Birthdate { get; init; }
    public string? Country { get; set; }
}

public record UpdateRiderDto()
{
    [Required] public string? Name { get; init; }
    [DateOnlyRange("1900-01-01", "2200-01-01")] public DateOnly Birthdate { get; init; }
    public string? Country { get; set; }
}