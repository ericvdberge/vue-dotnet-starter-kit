using WebGen.Infrastructure.Dao;

namespace WebGen.Infrastructure;

internal static class QueryableExtensions
{
    public static IQueryable<T> WithCursor<T>(
        this IQueryable<T> query,
        Guid cursor)
        where T : BaseDao
    {
        query = query
            .Where(x => x.Id > cursor)
            .OrderBy(x => x.Id);

        return query;
    }
}
