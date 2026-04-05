namespace WebGen.Api.Authentication;

/// <summary>
/// Configuration options for Keycloak token validation.
/// </summary>
public class KeycloakOptions
{
    /// <summary>
    /// The base URL of the Keycloak server (e.g., "https://keycloak.example.com").
    /// </summary>
    public required string Authority { get; set; }

    /// <summary>
    /// The client ID for the API in Keycloak.
    /// </summary>
    public required string ClientId { get; set; }

    /// <summary>
    /// The realm name in Keycloak.
    /// </summary>
    public required string Realm { get; set; }
}
