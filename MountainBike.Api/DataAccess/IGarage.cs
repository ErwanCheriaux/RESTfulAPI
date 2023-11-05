using MountainBike.Models;

namespace MountainBike.DataAccess;

public interface IGarage
{
    public IEnumerable<Bike> GetBikes();
    public Bike? GetBike(Guid id);
    public void CreateBike(Bike bike);
    public void UpdateBike(Bike bike);
    public void DeleteBike(Guid id);

    public Task<IEnumerable<Bike>> GetBikesAsync();
    public Task<Bike> GetBikeAsync(Guid id);
    public Task CreateBikeAsync(Bike bike);
    public Task UpdateBikeAsync(Bike bike);
    public Task DeleteBikeAsync(Guid id);
}