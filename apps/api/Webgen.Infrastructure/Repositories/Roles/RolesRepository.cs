using Microsoft.EntityFrameworkCore;
using Webgen.Infrastructure;
using WebGen.Infrastructure.Dao;

namespace WebGen.Infrastructure.Repositories.Roles;

public interface IRolesRepository
{
    public Task<RoleDao[]> GetRolesAsync();
}

public class RolesRepository(
    AppDbContext context) : IRolesRepository
{
    public async Task<RoleDao[]> GetRolesAsync()
    {
        return await context.Roles
            .ToArrayAsync();
    }
}
