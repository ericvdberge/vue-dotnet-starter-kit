namespace WebGen.Api.Cors;

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for adding CORS to the service collection.
/// </summary>
public static class CorsExtensions
{

    public const string SECURE_CORS = "SecureUrls";
    /// <summary>
    /// Adds a secure CORS policy that only allows origins configured in the "Cors:FrontendUrls" configuration
    /// section. This can be overridden by environment variables (e.g. "Cors__FrontendUrls__0").
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="policyName">The CORS policy name (default: "AllowFrontend").</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSecureCors(
        this IServiceCollection services,
        IConfiguration configuration,
        string policyName = SECURE_CORS)
    {
        var origins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

        if (origins.Length == 0)
            throw new InvalidOperationException("No frontend URLs configured for CORS. Please configure 'Cors:AllowedOrigins' in configuration or set environment variables (e.g. 'Cors__AllowedOrigins__0').");

        services.AddCors(options =>
        {
            options.AddPolicy(policyName, builder =>
            {
                builder
                    .WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}
