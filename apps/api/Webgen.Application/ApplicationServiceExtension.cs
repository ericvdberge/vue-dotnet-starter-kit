using Microsoft.Extensions.DependencyInjection;
using Webgen.Application.Services;
using WebGen.Application.Mappers;
using WebGen.Application.Services;

namespace WebGen.Application;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserMapper, UserMapper>()
            .AddScoped<IPermissionService, PermissionService>();

        return services;
    }
}