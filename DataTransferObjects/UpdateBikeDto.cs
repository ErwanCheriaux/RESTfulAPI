using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.DataTransferObjects;

public record UpdateBikeDto
{
    [Required]
    public string? Brand { get; init; }

    [Required]
    public string? Model { get; init; }

    [Required]
    [Range(1900, 2100)]
    public int Year { get; init; }

    public string? Material { get; init; }
    public string? Color { get; init; }
    public string? Size { get; init; }
    public string? SerialNumber { get; init; }
}