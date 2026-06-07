using Riok.Mapperly.Abstractions;
using WebGen.Api.Endpoints.Roles;
using WebGen.Domain.Entities;

namespace WebGen.Api.Mappers;

public interface IRoleMapper
{
    RoleDto ToDto(Role role);
}

[Mapper]
public sealed partial class RoleMapper: IRoleMapper
{
    public partial RoleDto ToDto(Role role);
}
