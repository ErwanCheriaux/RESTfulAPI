using MountainBike.Services.Entities;
using MountainBike.Services.Repositories;

namespace MountainBike.Services.Services;

public class BikeService : IBikeService
{
    private readonly IBikeRepository _bikeRepository;

    public BikeService(IBikeRepository bikeRepository)
    {
        _bikeRepository = bikeRepository;
    }

    public async Task CreateBikeAsync(BikeEntity bike)
    {
        await _bikeRepository.CreateBikeAsync(bike);
    }

    public async Task DeleteBikeAsync(Guid id)
    {
        await _bikeRepository.DeleteBikeAsync(id);
    }

    public async Task<BikeEntity> GetBikeAsync(Guid id)
    {
        return await _bikeRepository.GetBikeAsync(id);
    }

    public async Task<IEnumerable<BikeEntity>> GetBikesAsync()
    {
        return await _bikeRepository.GetBikesAsync();
    }

    public async Task<IEnumerable<BikeEntity>> GetBikesByRiderIdAsync(Guid riderId)
    {
        return (await _bikeRepository.GetBikesAsync())
            .Where(bike => bike.RiderId == riderId);
    }

    public async Task UpdateBikeAsync(BikeEntity bike)
    {
        await _bikeRepository.UpdateBikeAsync(bike);
    }
}