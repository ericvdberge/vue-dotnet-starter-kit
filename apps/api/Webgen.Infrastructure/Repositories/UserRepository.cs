using Microsoft.EntityFrameworkCore;
using Webgen.Infrastructure;
using WebGen.Infrastructure.Dao;

namespace WebGen.Infrastructure.Repositories;

public interface IUserRepository
{
    public Task<UserDao[]> GetUsersAsync(Guid? lastId, int? limit);
}

public class UserRepository(AppDbContext context) : IUserRepository
{
    private const int DEFAULT_PAGE_SIZE = 100;
    public async Task<UserDao[]> GetUsersAsync(Guid? lastId, int? limit)
    {
        var query = context.Users
            .AsQueryable();

        if(lastId.HasValue)
        {
            query = query.Where(q => q.Id > lastId.Value);
        }

        var users = await query
            .OrderBy(q => q.Id)
            .Take(limit ?? DEFAULT_PAGE_SIZE)
            .ToArrayAsync();

        return users;
    }
}
