using MountainBike.Services.Entities;
using MountainBike.Services.Repositories;

namespace MountainBike.Services.Services;

public class RiderService : IRiderService
{
    private readonly IRiderRepository _riderRepository;
    private readonly IBikeService _bikeService;

    public RiderService(IRiderRepository riderRepository, IBikeService bikeService)
    {
        _riderRepository = riderRepository;
        _bikeService = bikeService;
    }
    public async Task CreateRiderAsync(RiderEntity rider)
    {
        await _riderRepository.CreateRiderAsync(rider);
    }

    public async Task DeleteRiderAsync(Guid id)
    {
        // remove existing bikes assigned to rider
        var existingBikes = await _bikeService.GetBikesByRiderIdAsync(id);
        foreach (var bike in existingBikes)
        {
            bike.RiderId = null;
            await _bikeService.UpdateBikeAsync(bike);
        }

        await _riderRepository.DeleteRiderAsync(id);
    }

    public async Task<RiderEntity> GetRiderAsync(Guid id)
    {
        return await _riderRepository.GetRiderAsync(id);
    }

    public async Task<IEnumerable<RiderEntity>> GetRidersAsync()
    {
        return await _riderRepository.GetRidersAsync();
    }

    public async Task UpdateRiderAsync(RiderEntity rider)
    {
        await _riderRepository.UpdateRiderAsync(rider);
    }
}