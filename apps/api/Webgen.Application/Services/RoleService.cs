using WebGen.Application.Mappers;
using WebGen.Domain.Entities;
using WebGen.Infrastructure.Repositories.Roles;

namespace WebGen.Application.Services;

public interface IRoleService
{
    Task<Role[]> GetRolesAsync();
}

public class RoleService(
    IRolesRepository repository,
    IRoleMapper mapper) : IRoleService
{
    public async Task<Role[]> GetRolesAsync()
    {
        var roleDaos = await repository.GetRolesAsync();
        return [.. roleDaos.Select(mapper.ToRole)];
    }
}
