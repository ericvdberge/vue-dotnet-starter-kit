using WebGen.Api.Abstractions;
using WebGen.Api.Mappers;

namespace WebGen.Api;

public static class ApiServiceExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        // All endpoints
        services.Scan(scan =>
            scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo<IEndpoints>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        // normal DI
        services
            .AddSingleton<IUserMapper, UserMapper>()
            .AddSingleton<IRoleMapper, RoleMapper>();

        return services;
    }

    public static IEndpointRouteBuilder MapApi(this IEndpointRouteBuilder app)
    {
        var endpoints = app.ServiceProvider.GetServices<IEndpoints>();
        foreach (var endpoint in endpoints)
            endpoint.MapEndpoints(app);
        
        return app;
    }
}
