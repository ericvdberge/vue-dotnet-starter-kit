using Riok.Mapperly.Abstractions;
using WebGen.Api.Endpoints.Users;
using WebGen.Domain.Entities;

namespace WebGen.Api.Mappers;

public interface IUserMapper
{
    UserDto ToDto(User user);
}

[Mapper]
public sealed partial class UserMapper: IUserMapper
{
    public partial UserDto ToDto(User user);
}
