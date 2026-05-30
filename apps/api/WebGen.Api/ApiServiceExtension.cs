using WebGen.Api.Abstractions;

namespace WebGen.Api;

public static class ApiServiceExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo<IEndpoints>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
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
