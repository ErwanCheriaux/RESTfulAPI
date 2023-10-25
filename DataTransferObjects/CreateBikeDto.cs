using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.DataTransferObjects;

public record CreateBikeDto
{
    [Required]
    public string? Brand { get; init; }

    [Required]
    public string? Model { get; init; }

    [Required]
    [Range(1900, 2100)]
    public int Year { get; init; }

    [Required]
    public string? Material { get; init; }

    [Required]
    public string? Color { get; init; }

    [Required]
    public string? Size { get; init; }

    public string? SerialNumber { get; init; }
}