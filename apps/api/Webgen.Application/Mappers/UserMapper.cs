using Riok.Mapperly.Abstractions;
using WebGen.Domain.Entities;
using WebGen.Infrastructure.Dao;

namespace WebGen.Application.Mappers;

public interface IUserMapper
{
    User ToUser(UserDao userDao);
}

[Mapper]
public sealed partial class UserMapper : IUserMapper
{
    public partial User ToUser(UserDao userDao);
}
