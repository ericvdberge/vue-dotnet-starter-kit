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

        group.MapGet("/", GetAllRolesAsync);
        group.MapGet("/{roleId:guid}/permissions", GetRolePermissionsAsync);
    }

    private async Task<IResult> GetAllRolesAsync()
    {
        var roles = await roleService.GetRolesAsync();
        return Results.Ok<RoleDto[]>([..roles.Select(roleMapper.ToDto)]);
    }

    private async Task<IResult> GetRolePermissionsAsync(Guid roleId)
    {
        var permissions = await roleService.GetRolePermissionsAsync(roleId);
        return Results.Ok<PermissionDto[]>([..permissions.Select(permissionMapper.ToDto)]);
    }
}
