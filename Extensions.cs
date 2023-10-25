using RESTfulAPI.DataTransferObjects;
using RESTfulAPI.Models;

namespace RESTfulAPI;

public static class Extensions
{
    public static BikeDto AsDto(this Bike bike)
    {
        return new BikeDto
        {
            Id = bike.Id,
            Brand = bike.Brand,
            Model = bike.Model,
            Year = bike.Year,
            Material = bike.Material,
            Color = bike.Color,
            Size = bike.Size,
            SerialNumber = bike.SerialNumber
        };
    }
}