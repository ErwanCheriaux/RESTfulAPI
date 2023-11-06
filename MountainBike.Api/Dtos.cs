using System.ComponentModel.DataAnnotations;

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
    DateTimeOffset CreationDate);

public record CreateBikeDto(
    [Required] string? Brand,
    [Required] string? Model,
    [Range(1900, 2900)] int Year,
    string? Material,
    string? Color,
    string? Size,
    string? SerialNumber);

public record UpdateBikeDto(
    [Required] string? Brand,
    [Required] string? Model,
    [Range(1900, 2900)] int Year,
    string? Material,
    string? Color,
    string? Size,
    string? SerialNumber);