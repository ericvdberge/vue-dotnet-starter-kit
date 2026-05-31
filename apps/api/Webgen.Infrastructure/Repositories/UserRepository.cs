using Microsoft.EntityFrameworkCore;
using Webgen.Infrastructure;
using WebGen.Infrastructure.Dao;

namespace WebGen.Infrastructure.Repositories;

public interface IUserRepository
{
    public Task<UserDao[]> GetUsersAsync();
}

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<UserDao[]> GetUsersAsync()
    {
        var users = await context.Users
            .Select(r => new UserDao //todo: use mapper of mapperly
            {
                Name = r.Name,
                Email = r.Email
            })
            .ToArrayAsync();

        return users;
    }
}
