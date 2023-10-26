using RESTfulAPI.Models;

namespace RESTfulAPI.DataAccess;

public interface IGarage
{
    public IEnumerable<Bike> GetBikes();
    public Bike? GetBike(Guid id);
    public void CreateBike(Bike bike);
    public void UpdateBike(Bike bike);
    public void DeleteBike(Guid id);
}