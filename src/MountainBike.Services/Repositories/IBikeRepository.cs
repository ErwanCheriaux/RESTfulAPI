using MountainBike.Services.Entities;

namespace MountainBike.Services.Repositories;

public interface IBikeRepository
{
    public Task<IEnumerable<BikeEntity>> GetBikesAsync();
    public Task<BikeEntity> GetBikeAsync(Guid id);
    public Task CreateBikeAsync(BikeEntity bike);
    public Task UpdateBikeAsync(BikeEntity bike);
    public Task DeleteBikeAsync(Guid id);
}