using MountainBike.Services.Entities;
using MountainBike.Services.Repositories;

namespace MountainBike.Services.Services;

public class RiderService(IRiderRepository riderRepository) : IRiderService
{
    private readonly IRiderRepository _riderRepository = riderRepository;
    public async Task CreateRiderAsync(RiderEntity rider)
    {
        await _riderRepository.CreateRiderAsync(rider);
    }

    public async Task DeleteRiderAsync(Guid id)
    {
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