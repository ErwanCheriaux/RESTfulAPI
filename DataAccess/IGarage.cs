using RESTfulAPI.Models;

namespace RESTfulAPI.DataAccess;

public interface IGarage
{
    public IEnumerable<Bike> GetBikes();
    public Bike? GetBike(Guid id);
}