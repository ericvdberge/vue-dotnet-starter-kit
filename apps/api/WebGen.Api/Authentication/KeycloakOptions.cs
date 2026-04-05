namespace WebGen.Api.Authentication;

/// <summary>
/// Configuration options for Keycloak token validation.
/// </summary>
public class KeycloakOptions
{
    /// <summary>
    /// The base URL of the Keycloak server (e.g., "https://keycloak.example.com").
    /// Can be internal service name for Docker communication.
    /// </summary>
    public required string Authority { get; set; }

    /// <summary>
    /// The public issuer URL that appears in JWT tokens (e.g., "https://auth.example.com").
    /// Should match the URL that Keycloak presents to clients/browsers.
    /// </summary>
    public required string ValidIssuer { get; set; }

    /// <summary>
    /// The client ID for the API in Keycloak.
    /// </summary>
    public required string ClientId { get; set; }

    /// <summary>
    /// The realm name in Keycloak.
    /// </summary>
    public required string Realm { get; set; }
}
