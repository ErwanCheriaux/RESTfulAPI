using MountainBike.Services.Entities;

namespace MountainBike.Services.Repositories;

public interface IUserRepository
{
    public Task<IEnumerable<UserEntity>> GetUsersAsync();
    public Task<UserEntity> GetUserAsync(Guid id);
    public Task CreateUserAsync(UserEntity user);
    public Task UpdateUserAsync(UserEntity user);
    public Task DeleteUserAsync(Guid id);
}