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
    string? Name
);

public record RiderDetailsDto(
    Guid Id,
    string? Name,
    int Age,
    string? Country,
    int BikeCount,
    DateTimeOffset CreationDate
);

public record CreateRiderDto(
    [Required] string? Name,
    string? Country
)
{
    [DateOnlyRange("1900-01-01", "2200-01-01")]
    public DateOnly Birthdate { get; init; }
}


public record UpdateRiderDto(
    [Required] string? Name,
    string? Country
)
{
    [DateOnlyRange("1900-01-01", "2200-01-01")]
    public DateOnly Birthdate { get; init; }
}