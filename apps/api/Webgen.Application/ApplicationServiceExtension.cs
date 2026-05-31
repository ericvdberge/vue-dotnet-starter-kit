using Microsoft.Extensions.DependencyInjection;
using Webgen.Application.Services;
using WebGen.Application.Services;

namespace WebGen.Application;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IPermissionService, PermissionService>();

        return services;
    }
}