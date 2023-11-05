using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MountainBike.Controllers;
using MountainBike.DataAccess;
using MountainBike.Models;

namespace MountainBike.UnitTests;

public class BikeControllerTests
{
    [Fact]
    public async Task GetBikeAsync_WithUnexistingBike_ReturnsNotFound()
    {
        // Arrange
        var garageStub = new Mock<IGarage>();
        garageStub
            .Setup(garage => garage.GetBikeAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Bike?)null!);

        var loggerStub = new Mock<ILogger<BikeController>>();

        var controller = new BikeController(garageStub.Object, loggerStub.Object);

        // Act
        var result = await controller.GetBikeAsync(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}