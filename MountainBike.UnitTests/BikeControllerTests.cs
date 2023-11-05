using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MountainBike.Controllers;
using MountainBike.DataAccess;
using MountainBike.Models;

namespace MountainBike.UnitTests;

public class BikeControllerTests
{
    private readonly Mock<IGarage> _garageStub = new();
    private readonly Mock<ILogger<BikeController>> _loggerStub = new();
    private readonly Random random = new();

    [Fact]
    public async Task GetBikeAsync_WithUnexistingBike_ReturnsNotFound()
    {
        // Arrange
        _garageStub
            .Setup(garage => garage.GetBikeAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Bike?)null!);

        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.GetBikeAsync(Guid.NewGuid());

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetBikeAsync_WithExistingBike_ReturnsExpectedBike()
    {
        // Arrange
        var expectedBike = CreateRandomBike();

        _garageStub
            .Setup(garage => garage.GetBikeAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedBike);

        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.GetBikeAsync(Guid.NewGuid());

        // Assert
        result.Value.Should().BeEquivalentTo(
            expectedBike,
            option => option.ComparingByMembers<Bike>());
    }

    [Fact]
    public async Task GetBikesAsync_WithExistingBikes_ReturnsAllBikes()
    {
        // Arrange
        var expectedBikes = new[] { CreateRandomBike(), CreateRandomBike(), CreateRandomBike() };

        _garageStub
            .Setup(garage => garage.GetBikesAsync())
            .ReturnsAsync(expectedBikes);

        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var results = await controller.GetBikesAsync();

        // Assert
        results.Should().BeEquivalentTo(
            expectedBikes,
            option => option.ComparingByMembers<Bike>());
    }

    private Bike CreateRandomBike()
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
}