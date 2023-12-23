using MountainBike.Services.Entities;

namespace MountainBike.Api;

public static class Extensions
{
    public static UserDto AsDto(this UserEntity user)
    {
        return new(
            user.Email,
            user.Passwordhash
        );
    }
    public static BikeDto AsDto(this BikeEntity bike)
    {
        return new(
            bike.Id,
            bike.RiderId,
            bike.Brand,
            bike.Model,
            bike.Year,
            bike.Material,
            bike.Color,
            bike.Size,
            bike.SerialNumber,
            bike.CreationDate
            );
    }

    public static RiderDto AsDto(this RiderEntity rider)
    {
        return new(
            rider.Id,
            rider.Name,
            rider.Birthdate,
            rider.Country,
            rider.CreationDate
            );
    }

    public static int AsAge(this DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        int age = today.Year - date.Year;
        if (date.AddYears(age) > today) age--;
        return age;
    }
}