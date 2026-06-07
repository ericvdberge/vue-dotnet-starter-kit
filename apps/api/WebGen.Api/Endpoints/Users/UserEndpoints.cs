using Microsoft.AspNetCore.Mvc;
using WebGen.Api.Abstractions;
using WebGen.Api.Mappers;
using WebGen.Application.Services;

namespace WebGen.Api.Endpoints.Users;

public class UserEndpoints(
    IUserService userService,
    IUserMapper userMapper) : IEndpoints
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("users")
            .WithTags("Users");

        group.MapGet("/", GetUsersAsync);
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="lastId">the Id of the last user that you have fetched</param>
    /// <param name="limit">the amount of items you want to fetch at the same time. 100 = default</param>
    /// <returns></returns>
    private async Task<IResult> GetUsersAsync(
        [FromQuery] Guid? lastId, 
        [FromQuery]int? limit)
    {
        var users = await userService.GetUsersAsync(lastId, limit);
        var userDtos = users.Select(userMapper.ToDto);
        return TypedResults.Ok<UserDto[]>([..userDtos]);
    }
}
