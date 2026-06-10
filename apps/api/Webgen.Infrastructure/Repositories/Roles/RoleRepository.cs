using Microsoft.EntityFrameworkCore;
using Webgen.Infrastructure;
using WebGen.Infrastructure.Dao;

namespace WebGen.Infrastructure.Repositories.Roles;

public interface IRoleRepository
{
    public Task<RoleDao[]> GetRolesAsync();
    public Task<PermissionDao[]> GetRolePermissionsAsync(Guid roleId);
}

public class RoleRepository(
    AppDbContext context) : IRoleRepository
{
    public async Task<RoleDao[]> GetRolesAsync()
    {
        return await context.Roles
            .ToArrayAsync();
    }

    public async Task<PermissionDao[]> GetRolePermissionsAsync(Guid roleId)
    {
        return await context.Permissions
            .Where(p => p.RoleId == roleId)
            .ToArrayAsync();
    }
}
