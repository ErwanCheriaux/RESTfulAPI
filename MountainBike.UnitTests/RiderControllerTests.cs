using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MountainBike.Api.Controllers;
using MountainBike.Api.DataAccess;
using MountainBike.Api;
using MountainBike.Api.Models;

namespace MountainBike.UnitTests;

public class RiderControllerTests
{
    private readonly Mock<IGarage> _garageStub = new();
    private readonly Mock<ILogger<RiderController>> _loggerStub = new();
    private readonly Random random = new();

    [Fact]
    public async Task GetRiderAsync_WithUnexistingRider_ReturnsNotFound()
    {
        // Arrange
        _garageStub
            .Setup(garage => garage.GetRiderAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Rider?)null!);

        var controller = new RiderController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.GetRiderAsync(Guid.NewGuid());

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetRiderAsync_WithExistingRider_ReturnsExpectedRider()
    {
        // Arrange
        var expectedRider = CreateRandomRider();

        _garageStub
            .Setup(garage => garage.GetRiderAsync(expectedRider.Id))
            .ReturnsAsync(expectedRider);

        var controller = new RiderController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.GetRiderAsync(expectedRider.Id);

        // Assert
        result.Value.Should().BeEquivalentTo(expectedRider);
    }

    [Fact]
    public async Task GetRidersAsync_WithExistingRiders_ReturnsAllRiders()
    {
        // Arrange
        var expectedRiders = new[] { CreateRandomRider(), CreateRandomRider(), CreateRandomRider() };

        _garageStub
            .Setup(garage => garage.GetRidersAsync())
            .ReturnsAsync(expectedRiders);

        var controller = new RiderController(_garageStub.Object, _loggerStub.Object);

        // Act
        var results = await controller.GetRidersAsync();

        // Assert
        results.Should().BeEquivalentTo(expectedRiders);
    }

    [Fact]
    public async Task GetRidersAsync_WithMatchingRiderName_ReturnsMatchingRiderName()
    {
        // Arrange
        string matchingRiderName = "bra";
        var allRiders = new[]
        {
            new Rider(){Name = "Erwan"},
            new Rider(){Name = "Brad"},
            new Rider(){Name = "Lars"}
         };

        _garageStub
            .Setup(garage => garage.GetRidersAsync())
            .ReturnsAsync(allRiders);

        var controller = new RiderController(_garageStub.Object, _loggerStub.Object);

        // Act
        var results = await controller.GetRidersAsync(matchingRiderName);

        // Assert
        results.Should().OnlyContain(
            rider => rider.Name == allRiders[1].Name);
    }

    [Fact]
    public async Task CreateRiderAsync_WithRidertoCreate_ReturnsCreatedRider()
    {
        // Arrange
        var riderToCreate = CreateRandomCreateRiderDto();
        var controller = new RiderController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.CreateRiderAsync(riderToCreate);

        // Assert
        var createdRider = ((result.Result as CreatedAtActionResult)!.Value as RiderDto)!;
        createdRider.Should().BeEquivalentTo(riderToCreate);
        createdRider.Id.Should().NotBeEmpty();
        createdRider.CreationDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public async Task UpdateRiderAsync_WithUnexistingRider_ReturnsNotFound()
    {
        // Arrange
        var riderToUpdate = CreateRandomUpdateRiderDto();
        var controller = new RiderController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.UpdateRiderAsync(Guid.NewGuid(), riderToUpdate);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateRiderAsync_WithExistingRider_ReturnsNoContent()
    {
        // Arrange
        var existingrider = CreateRandomRider();
        var riderToUpdate = CreateRandomUpdateRiderDto();
        _garageStub
            .Setup(garage => garage.GetRiderAsync(existingrider.Id))
            .ReturnsAsync(existingrider);

        var controller = new RiderController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.UpdateRiderAsync(existingrider.Id, riderToUpdate);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteRiderAsync_WithUnexistingRider_ReturnsNotFound()
    {
        // Arrange
        var controller = new RiderController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.DeleteRiderAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteRiderAsync_WithExistingRider_ReturnsNoContent()
    {
        // Arrange
        var existingRider = CreateRandomRider();
        _garageStub
            .Setup(garage => garage.GetRiderAsync(existingRider.Id))
            .ReturnsAsync(existingRider);

        var controller = new RiderController(_garageStub.Object, _loggerStub.Object);

        // Act
        var result = await controller.DeleteRiderAsync(existingRider.Id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    private Rider CreateRandomRider()
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(),
            Age = random.Next(1900, 2100),
            Country = Guid.NewGuid().ToString(),
            CreationDate = DateTimeOffset.UtcNow
        };
    }

    private CreateRiderDto CreateRandomCreateRiderDto()
    {
        return new(
            Guid.NewGuid().ToString(),
            random.Next(1900, 2100),
            Guid.NewGuid().ToString()
        );
    }

    private UpdateRiderDto CreateRandomUpdateRiderDto()
    {
        return new(
            Guid.NewGuid().ToString(),
            random.Next(1900, 2100),
            Guid.NewGuid().ToString()
        );
    }
}