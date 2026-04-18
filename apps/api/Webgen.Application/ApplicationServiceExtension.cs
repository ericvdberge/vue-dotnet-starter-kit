using Microsoft.Extensions.DependencyInjection;
using Webgen.Application.Services;

namespace WebGen.Application;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddScoped<IPermissionService, PermissionService>();

        return services;
    }
}