using MountainBike.Api;
using MountainBike.Api.Models;

namespace MountainBike.UnitTests;

public static class CreateRandom
{
    private static readonly Random random = new();
    public static Bike Bike()
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Brand = Guid.NewGuid().ToString(),
            Model = Guid.NewGuid().ToString(),
            Year = random.Next(1900, 2100),
            Material = Guid.NewGuid().ToString(),
            Color = Guid.NewGuid().ToString(),
            Size = Guid.NewGuid().ToString(),
            SerialNumber = Guid.NewGuid().ToString(),
            CreationDate = DateTimeOffset.UtcNow
        };
    }

    public static CreateBikeDto CreateBikeDto()
    {
        return new(
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            random.Next(1900, 2100),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
        );
    }

    public static UpdateBikeDto UpdateBikeDto()
    {
        return new(
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            random.Next(1900, 2100),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
        );
    }

    public static Rider Rider()
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(),
            Birthdate = RandomDateOnly(),
            Country = Guid.NewGuid().ToString(),
            CreationDate = DateTimeOffset.UtcNow
        };
    }

    public static CreateRiderDto CreateRiderDto()
    {
        return new(
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
        )
        {
            Birthdate = RandomDateOnly(),
        };
    }

    public static UpdateRiderDto UpdateRiderDto()
    {
        return new(
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
        )
        {
            Birthdate = RandomDateOnly(),
        };
    }

    private static DateOnly RandomDateOnly()
    {
        var start = new DateTime(1900, 1, 1);
        int range = (DateTime.Today - start).Days;
        return DateOnly.FromDateTime(start.AddDays(random.Next(range)));
    }
}