namespace MountainBike.Api.Models;

public class Rider
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateOnly Birthdate { get; set; }
    public string? Country { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}