using MountainBike.Api.Models;

namespace MountainBike.Api;

public static class Extensions
{
    public static BikeDto AsDto(this Bike bike)
    {
        return new(
            bike.Id,
            bike.Brand,
            bike.Model,
            bike.Year,
            bike.Material,
            bike.Color,
            bike.Size,
            bike.SerialNumber,
            bike.CreationDate);
    }

    public static RiderDto AsDto(this Rider rider)
    {
        return new(
            rider.Id,
            rider.Name,
            rider.Birthdate.AsAge(),
            rider.Country,
            rider.CreationDate);
    }

    public static int AsAge(this DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        int age = today.Year - date.Year;
        if (date.AddYears(age) > today) age--;
        return age;
    }
}