using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MountainBike.Api.Controllers;
using MountainBike.Api;
using MountainBike.Services.Services;
using MountainBike.Services.Entities;

namespace MountainBike.UnitTests;

public class RiderControllerTests
{
    private readonly Mock<IRiderService> _mockRiderService = new();
    private readonly Mock<IBikeService> _mockBikeService = new();
    private readonly Mock<ILogger<RiderController>> _mockLogger = new();
    private readonly Random random = new();

    [Fact]
    public async Task GetRiderAsync_WithUnexistingRider_ReturnsNotFound()
    {
        // Arrange
        _mockRiderService
            .Setup(service => service.GetRiderAsync(It.IsAny<Guid>()))
            .ReturnsAsync((RiderEntity?)null!);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.GetRiderAsync(Guid.NewGuid());

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetRiderAsync_WithExistingRider_ReturnsExpectedRider()
    {
        // Arrange
        var expectedRider = CreateRandom.Rider();

        _mockRiderService
            .Setup(service => service.GetRiderAsync(expectedRider.Id))
            .ReturnsAsync(expectedRider);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.GetRiderAsync(expectedRider.Id);

        // Assert
        result.Value.Should().BeEquivalentTo(expectedRider, option => option.ExcludingMissingMembers());
    }

    [Fact]
    public async Task GetRidersAsync_WithExistingRiders_ReturnsAllRiders()
    {
        // Arrange
        var expectedRiders = new[] { CreateRandom.Rider(), CreateRandom.Rider(), CreateRandom.Rider() };

        _mockRiderService
            .Setup(service => service.GetRidersAsync())
            .ReturnsAsync(expectedRiders);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.GetRidersAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedRiders, option => option.ExcludingMissingMembers());
    }

    [Fact]
    public async Task GetRidersAsync_WithMatchingRiderName_ReturnsMatchingRiderName()
    {
        // Arrange
        string matchingRiderName = "bra";
        var allRiders = new[]
        {
            new RiderEntity(){Name = "Erwan"},
            new RiderEntity(){Name = "Brad"},
            new RiderEntity(){Name = "Lars"}
         };

        _mockRiderService
            .Setup(service => service.GetRidersAsync())
            .ReturnsAsync(allRiders);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.GetRidersAsync(matchingRiderName);

        // Assert
        result.Should().OnlyContain(rider => rider.Name == allRiders[1].Name);
    }

    [Fact]
    public async Task CreateRiderAsync_WithRidertoCreate_ReturnsCreatedRider()
    {
        // Arrange
        var riderToCreate = CreateRandom.CreateRiderDto();
        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.CreateRiderAsync(riderToCreate);

        // Assert
        result.Result.Should().BeEquivalentTo(riderToCreate, option => option.ExcludingMissingMembers());

        var createdRider = ((result.Result as CreatedAtActionResult)!.Value as RiderDetailsDto)!;
        createdRider.Id.Should().NotBeEmpty();
        createdRider.Age.Should().Be(riderToCreate.Birthdate.AsAge());
        createdRider.CreationDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public async Task UpdateRiderAsync_WithUnexistingRider_ReturnsNotFound()
    {
        // Arrange
        var riderToUpdate = CreateRandom.UpdateRiderDto();
        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.UpdateRiderAsync(Guid.NewGuid(), riderToUpdate);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateRiderAsync_WithExistingRider_ReturnsNoContent()
    {
        // Arrange
        var existingrider = CreateRandom.Rider();
        var riderToUpdate = CreateRandom.UpdateRiderDto();
        _mockRiderService
            .Setup(service => service.GetRiderAsync(existingrider.Id))
            .ReturnsAsync(existingrider);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.UpdateRiderAsync(existingrider.Id, riderToUpdate);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteRiderAsync_WithUnexistingRider_ReturnsNotFound()
    {
        // Arrange
        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.DeleteRiderAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteRiderAsync_WithExistingRider_ReturnsNoContent()
    {
        // Arrange
        var existingRider = CreateRandom.Rider();
        _mockRiderService
            .Setup(service => service.GetRiderAsync(existingRider.Id))
            .ReturnsAsync(existingRider);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.DeleteRiderAsync(existingRider.Id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task GetRiderBikesAsync_WithUnexistingRider_ReturnsEmptyList()
    {
        // Arrange
        _mockBikeService
            .Setup(service => service.GetBikesAsync())
            .ReturnsAsync(Array.Empty<BikeEntity>());

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.GetRiderBikesAsync(Guid.NewGuid());

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetRiderBikesAsync_WithExistingRider_ReturnsExpectedBikes()
    {
        // Arrange
        var riderId = Guid.NewGuid();
        var expectedBikes = new[] { CreateRandom.Bike(), CreateRandom.Bike(), CreateRandom.Bike() };
        foreach (var bike in expectedBikes)
        {
            bike.RiderId = riderId;
        }

        _mockBikeService
            .Setup(service => service.GetBikesByRiderIdAsync(riderId))
            .ReturnsAsync(expectedBikes);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.GetRiderBikesAsync(riderId);

        // Assert
        result.Should().BeEquivalentTo(expectedBikes.Select(bike => bike.AsDto()));
    }

    [Fact]
    public async Task AddRiderBikeAsync_WithUnexistingRider_ReturnsNotFound()
    {
        // Arrange
        _mockRiderService
            .Setup(service => service.GetRiderAsync(It.IsAny<Guid>()))
            .ReturnsAsync((RiderEntity?)null!);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.AddRiderBikeAsync(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task AddRiderBikeAsync_WithUnexistingBike_ReturnsNotFound()
    {
        // Arrange
        _mockBikeService
            .Setup(service => service.GetBikeAsync(It.IsAny<Guid>()))
            .ReturnsAsync((BikeEntity?)null!);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.AddRiderBikeAsync(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task AddRiderBikeAsync_WithExistingRiderAndBike_ReturnsNoContent()
    {
        // Arrange
        var existingRider = CreateRandom.Rider();
        var existingBike = CreateRandom.Bike();

        _mockRiderService
            .Setup(service => service.GetRiderAsync(existingRider.Id))
            .ReturnsAsync(existingRider);

        _mockBikeService
            .Setup(service => service.GetBikeAsync(existingBike.Id))
            .ReturnsAsync(existingBike);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.AddRiderBikeAsync(existingRider.Id, existingBike.Id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        existingBike.RiderId.Should().Be(existingRider.Id);
    }

    [Fact]
    public async Task RemoveRiderBikeAsync_WithUnexistingRider_ReturnsNotFound()
    {
        // Arrange
        _mockRiderService
            .Setup(service => service.GetRiderAsync(It.IsAny<Guid>()))
            .ReturnsAsync((RiderEntity?)null!);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.RemoveRiderBikeAsync(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task RemoveRiderBikeAsync_WithUnexistingBike_ReturnsNotFound()
    {
        // Arrange
        _mockBikeService
            .Setup(service => service.GetBikeAsync(It.IsAny<Guid>()))
            .ReturnsAsync((BikeEntity?)null!);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.RemoveRiderBikeAsync(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task RemoveRiderBikeAsync_WithExistingRiderAndBike_ReturnsNoContent()
    {
        // Arrange
        var existingRider = CreateRandom.Rider();
        var existingBike = CreateRandom.Bike();

        _mockRiderService
            .Setup(service => service.GetRiderAsync(existingRider.Id))
            .ReturnsAsync(existingRider);

        _mockBikeService
            .Setup(service => service.GetBikeAsync(existingBike.Id))
            .ReturnsAsync(existingBike);

        var controller = new RiderController(_mockRiderService.Object, _mockBikeService.Object, _mockLogger.Object);

        // Act
        var result = await controller.RemoveRiderBikeAsync(existingRider.Id, existingBike.Id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        existingBike.RiderId.Should().BeNull();
    }
}