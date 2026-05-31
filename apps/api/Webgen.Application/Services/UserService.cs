using WebGen.Domain.Entities;
using WebGen.Infrastructure.Repositories;

namespace WebGen.Application.Services;

public interface IUserService
{
    Task<User[]> GetUsersAsync();
}

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<User[]> GetUsersAsync()
    {
        var userDaos = await userRepository.GetUsersAsync();
        return [..userDaos.Select(dao => new User { Name = dao.Name, Email = dao.Email})]; //todo: use mapper of mapperly
    }
}
