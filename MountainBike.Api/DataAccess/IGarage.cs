using MountainBike.Api.Models;

namespace MountainBike.Api.DataAccess;

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

    public Task<IEnumerable<Rider>> GetRidersAsync();
    public Task<Rider> GetRiderAsync(Guid id);
    public Task CreateRiderAsync(Rider rider);
    public Task UpdateRiderAsync(Rider rider);
    public Task DeleteRiderAsync(Guid id);
}