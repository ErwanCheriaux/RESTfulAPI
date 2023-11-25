using MountainBike.Services.Entities;

namespace MountainBike.Services.Services;

public interface IBikeService
{
    public Task<IEnumerable<BikeEntity>> GetBikesAsync();
    public Task<IEnumerable<BikeEntity>> GetBikesByRiderIdAsync(Guid riderId);
    public Task<BikeEntity> GetBikeAsync(Guid id);
    public Task CreateBikeAsync(BikeEntity bike);
    public Task UpdateBikeAsync(BikeEntity bike);
    public Task DeleteBikeAsync(Guid id);
}