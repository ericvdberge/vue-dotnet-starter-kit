using WebGen.Api.Abstractions;
using WebGen.Api.Mappers;
using WebGen.Application.Services;

namespace WebGen.Api.Endpoints.Roles;

public class RoleEndpoints(
    IRoleService roleService,
    IRoleMapper roleMapper,
    IPermissionMapper permissionMapper) : IEndpoints
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("roles")
            .WithTags("Roles");

        group
            .MapGet("/", GetAllRolesAsync)
            .WithDescription("Get all roles present in the system");
        group
            .MapGet("/{roleId:guid}/permissions", GetRolePermissionsAsync)
            .WithDescription("Get permissions of a specific role");
    }

    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns>All roles present in the system</returns>
    private async Task<IResult> GetAllRolesAsync()
    {
        var roles = await roleService.GetRolesAsync();
        return Results.Ok<RoleDto[]>([..roles.Select(roleMapper.ToDto)]);
    }

    /// <summary>
    /// Get permissions of a role
    /// </summary>
    /// <param name="roleId">The id of the role you want permissions for</param>
    /// <returns>Permissions associated with the role</returns>
    private async Task<IResult> GetRolePermissionsAsync(Guid roleId)
    {
        var permissions = await roleService.GetRolePermissionsAsync(roleId);
        return Results.Ok<PermissionDto[]>([..permissions.Select(permissionMapper.ToDto)]);
    }
}
