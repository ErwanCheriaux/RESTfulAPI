using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MountainBike.Controllers;
using MountainBike.DataAccess;
using MountainBike.DataTransferObjects;
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
            .Setup(garage => garage.GetBikeAsync(expectedBike.Id))
            .ReturnsAsync(expectedBike);

        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.GetBikeAsync(expectedBike.Id);

        // Assert
        result.Value.Should().BeEquivalentTo(
            expectedBike,
            option => option.ComparingByMembers<BikeDto>());
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
            option => option.ComparingByMembers<BikeDto>());
    }

    [Fact]
    public async Task CreateBikeAsync_WithBiketoCreate_ReturnsCreatedBike()
    {
        // Arrange
        var bikeToCreate = CreateRandomCreateBikeDto();
        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.CreateBikeAsync(bikeToCreate);

        // Assert
        var createdBike = ((result.Result as CreatedAtActionResult)!.Value as BikeDto)!;
        createdBike.Should().BeEquivalentTo(
            bikeToCreate,
            option => option.ComparingByMembers<BikeDto>());
        createdBike.Id.Should().NotBeEmpty();
        createdBike.CreationDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public async Task UpdateBikeAsync_WithUnexistingBike_ReturnsNotFound()
    {
        // Arrange
        var bikeToUpdate = CreateRandomUpdateBikeDto();
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
        var existingbike = CreateRandomBike();
        var bikeToUpdate = CreateRandomUpdateBikeDto();
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
        var existingBike = CreateRandomBike();
        _garageStub
            .Setup(garage => garage.GetBikeAsync(existingBike.Id))
            .ReturnsAsync(existingBike);

        var controller = new BikeController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.DeleteBikeAsync(existingBike.Id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
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

    private CreateBikeDto CreateRandomCreateBikeDto()
    {
        return new()
        {
            Brand = Guid.NewGuid().ToString(),
            Model = Guid.NewGuid().ToString(),
            Year = random.Next(1900, 2100),
            Material = Guid.NewGuid().ToString(),
            Color = Guid.NewGuid().ToString(),
            Size = Guid.NewGuid().ToString(),
            SerialNumber = Guid.NewGuid().ToString()
        };
    }

    private UpdateBikeDto CreateRandomUpdateBikeDto()
    {
        return new()
        {
            Brand = Guid.NewGuid().ToString(),
            Model = Guid.NewGuid().ToString(),
            Year = random.Next(1900, 2100),
            Material = Guid.NewGuid().ToString(),
            Color = Guid.NewGuid().ToString(),
            Size = Guid.NewGuid().ToString(),
            SerialNumber = Guid.NewGuid().ToString()
        };
    }
}