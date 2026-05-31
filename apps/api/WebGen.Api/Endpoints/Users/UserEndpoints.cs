using Microsoft.AspNetCore.Http.HttpResults;
using WebGen.Api.Abstractions;
using WebGen.Application.Services;

namespace WebGen.Api.Endpoints.Users;

public class UserEndpoints(
    IUserService userService) : IEndpoints
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("users")
            .WithTags("Users");
        group.MapGet("/", GetUsersAsync);
    }

    private async Task<Ok<UserDto[]>> GetUsersAsync()
    {
        var users = await userService.GetUsersAsync();
        var userDtos = users.Select(u => new UserDto { Name = u.Name, Email = u.Email }).ToArray(); //todo: Use mapper from mapperly
        return TypedResults.Ok(userDtos);
    }
}
