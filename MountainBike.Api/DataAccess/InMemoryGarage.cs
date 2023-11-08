using MountainBike.Api.Models;

namespace MountainBike.Api.DataAccess;

public class InMemoryGarage : IGarage
{
    private readonly List<Bike> _bikes = new()
    {
        new Bike {Id=Guid.NewGuid(), Brand="Santa cruz", Model="Tall boy", Year=2015, Color="Blue", Material="Alloy", Size="XL", SerialNumber="N/A"},
        new Bike {Id=Guid.NewGuid(), Brand="Transition", Model="Patrol", Year=2019, Color="Blue", Material="Alloy", Size="XL", SerialNumber="TBC4589652"},
        new Bike {Id=Guid.NewGuid(), Brand="Pivot", Model="Firebird", Year=2023, Color="Orange", Material="Alloy", Size="L", SerialNumber="48754 62321"}
    };

    public IEnumerable<Bike> GetBikes()
    {
        return _bikes;
    }

    public Bike? GetBike(Guid id)
    {
        return _bikes.SingleOrDefault(bike => bike.Id == id);
    }

    public void CreateBike(Bike bike)
    {
        _bikes.Add(bike);
    }

    public void UpdateBike(Bike bike)
    {
        var index = _bikes.FindIndex(existingBike => existingBike.Id == bike.Id);
        _bikes[index] = bike;
    }

    public void DeleteBike(Guid id)
    {
        var index = _bikes.FindIndex(existingBike => existingBike.Id == id);
        _bikes.RemoveAt(index);
    }

    public Task<IEnumerable<Bike>> GetBikesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Bike> GetBikeAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateBikeAsync(Bike bike)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBikeAsync(Bike bike)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBikeAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Rider>> GetRidersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Rider> GetRiderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateRiderAsync(Rider rider)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRiderAsync(Rider rider)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRiderAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}