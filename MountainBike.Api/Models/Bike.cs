namespace MountainBike.Api.Models;

public class Bike
{
    public Guid Id { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int Year { get; set; }
    public string? Material { get; set; }
    public string? Color { get; set; }
    public string? Size { get; set; }
    public string? SerialNumber { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}