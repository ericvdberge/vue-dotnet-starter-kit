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
    [MapperIgnoreSource(nameof(User.Active))] // do not expose the Active property in the DTO
    public partial UserDto ToDto(User user);
}
