namespace MountainBike.Api.Models;

public class Rider
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    private int _yearOfBirth;
    public int Age
    {
        get { return DateTime.Now.Year + _yearOfBirth; }
        set { _yearOfBirth = DateTime.Now.Year - Age; }
    }
    public string? Country { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}