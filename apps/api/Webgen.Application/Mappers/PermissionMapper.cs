using Riok.Mapperly.Abstractions;
using WebGen.Domain.Entities;
using WebGen.Infrastructure.Dao;

namespace WebGen.Application.Mappers;

public interface IPermissionMapper
{
    Permission ToPermission(PermissionDao permissionDao);
}

[Mapper]
public partial class PermissionMapper : IPermissionMapper
{
    [MapperIgnoreSource(nameof(PermissionDao.RoleId))]
    public partial Permission ToPermission(PermissionDao permissionDao);
}
