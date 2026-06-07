using Riok.Mapperly.Abstractions;
using WebGen.Domain.Entities;
using WebGen.Infrastructure.Dao;

namespace WebGen.Application.Mappers;

public interface IRoleMapper
{
    Role ToRole(RoleDao roleDao);
}

[Mapper]
public sealed partial class RoleMapper : IRoleMapper
{
    public partial Role ToRole(RoleDao roleDao);
}
