using MountainBike.Services.Entities;

namespace MountainBike.Services.Repositories;

public interface IRiderRepository
{
    public Task<IEnumerable<RiderEntity>> GetRidersAsync();
    public Task<RiderEntity> GetRiderAsync(Guid id);
    public Task CreateRiderAsync(RiderEntity rider);
    public Task UpdateRiderAsync(RiderEntity rider);
    public Task DeleteRiderAsync(Guid id);
}