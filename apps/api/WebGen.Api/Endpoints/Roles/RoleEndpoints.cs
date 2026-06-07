using WebGen.Api.Abstractions;
using WebGen.Api.Mappers;
using WebGen.Application.Services;

namespace WebGen.Api.Endpoints.Roles;

public class RoleEndpoints(
    IRoleService roleService,
    IRoleMapper mapper) : IEndpoints
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("roles")
            .WithTags("Roles");

        group.MapGet("/", GetAllRolesAsync);
    }

    private async Task<IResult> GetAllRolesAsync()
    {
        var roles = await roleService.GetRolesAsync();
        return TypedResults.Ok<RoleDto[]>([..roles.Select(mapper.ToDto)]);
    }
}
