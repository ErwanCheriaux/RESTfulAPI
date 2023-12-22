using MountainBike.Services.Entities;
using MountainBike.Services.Repositories;

namespace MountainBike.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CreateUserAsync(UserEntity user)
    {
        await _userRepository.CreateUserAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _userRepository.DeleteUserAsync(id);
    }

    public async Task<UserEntity> GetUserAsync(Guid id)
    {
        return await _userRepository.GetUserAsync(id);
    }

    public async Task<IEnumerable<UserEntity>> GetUsersAsync()
    {
        return await _userRepository.GetUsersAsync();
    }

    public async Task UpdateUserAsync(UserEntity user)
    {
        await _userRepository.UpdateUserAsync(user);
    }
}