using WebGen.Application.Mappers;
using WebGen.Domain.Entities;
using WebGen.Infrastructure.Repositories;

namespace WebGen.Application.Services;

public interface IUserService
{
    Task<User[]> GetUsersAsync(Guid? lastId, int? limit);
}

public class UserService(
    IUserRepository userRepository,
    IUserMapper userMapper) : IUserService
{
    public async Task<User[]> GetUsersAsync(Guid? lastId, int? limit)
    {
        var userDaos = await userRepository.GetUsersAsync(lastId, limit);
        return [.. userDaos.Select(userMapper.ToUser)];
    }
}
