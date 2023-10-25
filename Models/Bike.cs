namespace RESTfulAPI.Models;

public record Bike
{
    public Guid Id { get; init; }
    public string? Brand { get; init; }
    public string? Model { get; init; }
    public int Year { get; init; }
    public string? Material { get; init; }
    public string? Color { get; init; }
    public string? Size { get; init; }
    public string? SerialNumber { get; init; }
}