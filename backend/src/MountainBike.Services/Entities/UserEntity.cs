namespace MountainBike.Services.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Passwordhash { get; set; } = string.Empty;
    public DateTimeOffset CreationDate { get; set; }
}