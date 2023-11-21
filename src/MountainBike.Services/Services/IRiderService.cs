using MountainBike.Services.Entities;

namespace MountainBike.Services.Services;

public interface IRiderService
{
    public Task<IEnumerable<RiderEntity>> GetRidersAsync();
    public Task<RiderEntity> GetRiderAsync(Guid id);
    public Task CreateRiderAsync(RiderEntity rider);
    public Task UpdateRiderAsync(RiderEntity rider);
    public Task DeleteRiderAsync(Guid id);
}