using Microsoft.AspNetCore.Http.HttpResults;
using WebGen.Api.Abstractions;

namespace WebGen.Api.Endpoints.Users;

public class UserEndpoints : IEndpoints
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("users")
            .WithTags("Users");
        group.MapGet("/", GetUsers);
    }

    private Ok<UserDto[]> GetUsers()
    {
        return TypedResults.Ok<UserDto[]>([]);
    }
}
