using RESTfulAPI.Models;

namespace RESTfulAPI.DataAccess;

public class Garage : IGarage
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
}