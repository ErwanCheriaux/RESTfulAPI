using MountainBike.Services.Entities;

namespace MountainBike.Services.Services;

public interface IUserService
{
    public Task<IEnumerable<UserEntity>> GetUsersAsync();
    public Task<UserEntity> GetUserAsync(Guid id);
    public Task CreateUserAsync(UserEntity user);
    public Task UpdateUserAsync(UserEntity user);
    public Task DeleteUserAsync(Guid id);
}