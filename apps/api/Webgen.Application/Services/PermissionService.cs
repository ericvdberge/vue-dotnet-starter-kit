using Webgen.Infrastructure;
using WebGen.Domain.Entities;

namespace Webgen.Application.Services;

public interface IPermissionService
{
    public Task<bool> HasPermission(Guid userId, string action, string resource);
    public Task AssignUserRole(Guid userId, Guid roleId);
}

public class PermissionService(
    IUnitOfWork _unitOfWork    
) : IPermissionService
{
    public async Task AssignUserRole(Guid userId, Guid roleId)
    {
        var user = new User { Email = "", Id = userId }; //get from context

        user.AssignRole(roleId);
        await _unitOfWork.SaveChangesAsync();
    }

    public Task<bool> HasPermission(Guid userId, string action, string resource)
    {
        throw new NotImplementedException();
    }
}
