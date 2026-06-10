using System.Security;
using WebGen.Application.Mappers;
using WebGen.Domain.Entities;
using WebGen.Infrastructure.Repositories.Roles;

namespace WebGen.Application.Services;

public interface IRoleService
{
    Task<Role[]> GetRolesAsync();
    Task<Permission[]> GetRolePermissionsAsync(Guid roleId);
}

public class RoleService(
    IRoleRepository repository,
    IRoleMapper roleMapper,
    IPermissionMapper permissionMapper) : IRoleService
{
    public async Task<Role[]> GetRolesAsync()
    {
        var roleDaos = await repository.GetRolesAsync();
        return [.. roleDaos.Select(roleMapper.ToRole)];
    }

    public async Task<Permission[]> GetRolePermissionsAsync(Guid roleId)
    {
        var permissionDaos = await repository.GetRolePermissionsAsync(roleId);
        return [.. permissionDaos.Select(permissionMapper.ToPermission)];
    }
}
