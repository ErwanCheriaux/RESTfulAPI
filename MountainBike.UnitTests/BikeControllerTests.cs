using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MountainBike.Api.Controllers;
using MountainBike.Api.DataAccess;
using MountainBike.Api;
using MountainBike.Api.Models;

namespace MountainBike.UnitTests;

public class BikeControllerTests
{
    private readonly Mock<IGarage> _garageStub = new();
    private readonly Mock<ILogger<BikeController>> _loggerStub = new();

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
        var expectedBike = CreateRandom.Bike();

        _garageStub
            .Setup(garage => garage.GetBikeAsync(expectedBike.Id))
            .ReturnsAsync(expectedBike);

        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.GetBikeAsync(expectedBike.Id);

        // Assert
        result.Value.Should().BeEquivalentTo(expectedBike.AsDto());
    }

    [Fact]
    public async Task GetBikesAsync_WithExistingBikes_ReturnsAllBikes()
    {
        // Arrange
        var expectedBikes = new[] { CreateRandom.Bike(), CreateRandom.Bike(), CreateRandom.Bike() };

        _garageStub
            .Setup(garage => garage.GetBikesAsync())
            .ReturnsAsync(expectedBikes);

        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var results = await controller.GetBikesAsync();

        // Assert
        results.Should().BeEquivalentTo(expectedBikes.Select(bike => bike.AsDto()));
    }

    [Fact]
    public async Task GetBikesAsync_WithMatchingBikeBrand_ReturnsMatchingBikeBrand()
    {
        // Arrange
        string matchingBikeBrand = "santa";
        var allBikes = new[]
        {
            new Bike(){Brand = "Santa Cruz"},
            new Bike(){Brand = "Yeti"},
            new Bike(){Brand = "santa monica"}
         };

        _garageStub
            .Setup(garage => garage.GetBikesAsync())
            .ReturnsAsync(allBikes);

        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var results = await controller.GetBikesAsync(matchingBikeBrand);

        // Assert
        results.Should().OnlyContain(
            bike => bike.Brand == allBikes[0].Brand || bike.Brand == allBikes[2].Brand);
    }

    [Fact]
    public async Task CreateBikeAsync_WithBiketoCreate_ReturnsCreatedBike()
    {
        // Arrange
        var bikeToCreate = CreateRandom.CreateBikeDto();
        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.CreateBikeAsync(bikeToCreate);

        // Assert
        var createdBike = ((result.Result as CreatedAtActionResult)!.Value as BikeDto)!;
        createdBike.Should().BeEquivalentTo(bikeToCreate);
        createdBike.Id.Should().NotBeEmpty();
        createdBike.CreationDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public async Task UpdateBikeAsync_WithUnexistingBike_ReturnsNotFound()
    {
        // Arrange
        var bikeToUpdate = CreateRandom.UpdateBikeDto();
        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.UpdateBikeAsync(Guid.NewGuid(), bikeToUpdate);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateBikeAsync_WithExistingBike_ReturnsNoContent()
    {
        // Arrange
        var existingbike = CreateRandom.Bike();
        var bikeToUpdate = CreateRandom.UpdateBikeDto();
        _garageStub
            .Setup(garage => garage.GetBikeAsync(existingbike.Id))
            .ReturnsAsync(existingbike);

        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.UpdateBikeAsync(existingbike.Id, bikeToUpdate);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteBikeAsync_WithUnexistingBike_ReturnsNotFound()
    {
        // Arrange
        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.DeleteBikeAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteBikeAsync_WithExistingBike_ReturnsNoContent()
    {
        // Arrange
        var existingBike = CreateRandom.Bike();
        _garageStub
            .Setup(garage => garage.GetBikeAsync(existingBike.Id))
            .ReturnsAsync(existingBike);

        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.DeleteBikeAsync(existingBike.Id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}