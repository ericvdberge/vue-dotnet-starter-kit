using Microsoft.EntityFrameworkCore;
using Webgen.Infrastructure;
using WebGen.Infrastructure.Dao;

namespace WebGen.Infrastructure.Repositories.Users;

public interface IUserRepository
{
    public Task<UserDao[]> GetUsersAsync(Guid? lastId, int? limit);
}

public class UserRepository(AppDbContext context) : IUserRepository
{
    private const int DEFAULT_PAGE_SIZE = 100;

    /// <summary>
    /// Gets a paginated list of users. If lastId is provided, 
    /// it will return users with IDs greater than lastId.
    /// </summary>
    /// <param name="lastId">The ID of the last user retrieved in the previous page.</param>
    /// <param name="limit">The maximum number of users to retrieve.</param>
    /// <returns>An array of UserDao objects.</returns>
    public async Task<UserDao[]> GetUsersAsync(Guid? lastId, int? limit)
    {
        var query = context.Users
            .AsQueryable()
            .IsActive();

        if(lastId.HasValue)
            query = query.WithCursor(lastId.Value);

        var users = await query
            .Take(limit ?? DEFAULT_PAGE_SIZE)
            .ToArrayAsync();

        return users;
    }
}
