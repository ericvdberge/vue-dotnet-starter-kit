namespace WebGen.Api.Cors;

/// <summary>
/// Extension methods for adding permissive CORS to the service collection.
/// </summary>
public static class CorsExtensions
{
    /// <summary>
    /// Adds a permissive CORS policy that allows all origins, methods, and headers.
    /// WARNING: Only use in development. Do not use in production without restricting origins.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="policyName">The CORS policy name (default: "AllowAll").</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddPermissiveCors(
        this IServiceCollection services,
        string policyName = "AllowAll")
    {
        services.AddCors(options =>
        {
            options.AddPolicy(policyName, builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}
