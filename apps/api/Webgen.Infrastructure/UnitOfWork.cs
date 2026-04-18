namespace Webgen.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class UnitOfWork(AppDbContext _context) : IUnitOfWork
{
    public void Dispose()
        => _context.Dispose();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}


