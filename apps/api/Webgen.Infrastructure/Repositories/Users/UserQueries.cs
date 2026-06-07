using WebGen.Infrastructure.Dao;

namespace WebGen.Infrastructure.Repositories.Users;

internal static class UserQueries
{
    /// <summary>
    /// Filters the query to only include active users.
    /// </summary>
    public static IQueryable<UserDao> IsActive(this IQueryable<UserDao> query)
    {
        return query.Where(q => q.Active);
    }
}
