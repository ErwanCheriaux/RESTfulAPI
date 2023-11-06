using MountainBike.Api.Models;

namespace MountainBike.Api;

public static class Extensions
{
    public static BikeDto AsDto(this Bike bike)
    {
        return new(bike.Id, bike.Brand, bike.Model, bike.Year, bike.Material, bike.Color, bike.Size, bike.SerialNumber, bike.CreationDate);
    }
}