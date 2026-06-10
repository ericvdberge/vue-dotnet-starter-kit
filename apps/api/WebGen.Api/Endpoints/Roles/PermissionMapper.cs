using Riok.Mapperly.Abstractions;
using WebGen.Api.Endpoints.Roles;
using WebGen.Domain.Entities;

namespace WebGen.Api.Mappers;

public interface IPermissionMapper
{
    PermissionDto ToDto(Permission permission);
}

[Mapper]
public sealed partial class PermissionMapper: IPermissionMapper
{
    public partial PermissionDto ToDto(Permission permission);
}
