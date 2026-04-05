using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebGen.Api.Authentication;

/// <summary>
/// Extension methods for adding Keycloak authentication to the service collection.
/// </summary>
public static class KeycloakAuthenticationExtensions
{
    /// <summary>
    /// Adds Keycloak JWT bearer token authentication using configuration from IConfiguration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="sectionName">The configuration section name containing Keycloak options (default: "Keycloak").</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddKeycloakAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = "Keycloak")
    {
        var options = configuration.GetSection(sectionName).Get<KeycloakOptions>()
            ?? throw new InvalidOperationException(
                $"Keycloak configuration section '{sectionName}' not found or invalid in appsettings.json");

        var authority = $"{options.Authority}/realms/{options.Realm}";

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(config =>
            {
                config.Authority = authority;
                config.Audience = options.ClientId;
                config.MetadataAddress = $"{options.Authority}/realms/{options.Realm}/.well-known/openid-configuration";
                config.RequireHttpsMetadata = !options.Authority.StartsWith("http://");

                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuers = new[]
                    {
                        authority,
                        authority.Replace("keycloak", "localhost")
                    },
                    ValidateAudience = true,
                    ValidAudience = options.ClientId,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };

                config.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context => 
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<KeycloakOptions>>();
                        logger.LogError(context.Exception, "Authentication failed for {Path}", context.HttpContext.Request.Path);
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}

