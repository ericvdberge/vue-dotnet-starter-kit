# Keycloak Authentication Service

This authentication service extension provides JWT bearer token validation for Keycloak in the minimal API.

## Features

- **JWT Bearer Token Validation**: Validates tokens issued by Keycloak
- **Configurable Authority and Realm**: Easy setup for any Keycloak instance
- **Logging**: Built-in logging for authentication failures and token validation events
- **OpenID Connect Metadata Discovery**: Automatically discovers signing keys from Keycloak

## Configuration

Add Keycloak settings to `appsettings.json`:

```json
{
  "Keycloak": {
    "Authority": "https://your-keycloak-server.com",
    "ClientId": "your-api-client-id",
    "Realm": "your-realm-name"
  }
}
```

## Usage

### 1. Register the Authentication Service

In `Program.cs`:

```csharp
using WebGen.Api.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add Keycloak authentication
builder.Services.AddKeycloakAuthentication(builder.Configuration);

// ... rest of configuration
```

### 2. Use Authorization on Endpoints

Protect endpoints with the `[Authorize]` attribute:

```csharp
using Microsoft.AspNetCore.Authorization;

app.MapGet("/api/protected", () => "This endpoint requires authentication")
    .RequireAuthorization()
    .WithName("Protected Endpoint")
    .WithOpenApi();
```

### 3. Access Token Claims

Access the authenticated user's claims in your handlers:

```csharp
app.MapGet("/api/me", (HttpContext context) => 
{
    var userId = context.User.FindFirst("sub")?.Value;
    var userName = context.User.FindFirst("preferred_username")?.Value;
    
    return new { userId, userName };
})
.RequireAuthorization();
```

## Token Claims

Common Keycloak token claims:

- `sub`: Subject (user ID)
- `preferred_username`: Username
- `email`: User email
- `email_verified`: Email verification status
- `name`: Full name
- `given_name`: First name
- `family_name`: Last name
- `realm_access.roles`: User roles

## Example Protected Endpoint

```csharp
using Microsoft.AspNetCore.Authorization;

app.MapGet("/api/user-info", 
    [Authorize] (HttpContext context) => 
    {
        return new 
        {
            Subject = context.User.FindFirst("sub")?.Value,
            Username = context.User.FindFirst("preferred_username")?.Value,
            Email = context.User.FindFirst("email")?.Value,
            Roles = context.User.FindAll("realm_access")
        };
    })
    .WithOpenApi();
```

## Testing

To test with a Keycloak token, add the JWT to the Authorization header:

```bash
curl -H "Authorization: Bearer YOUR_JWT_TOKEN" https://localhost:5001/api/user-info
```

## Troubleshooting

### Authentication Failed Errors

Check that:
1. Keycloak server is running and accessible
2. Authority URL is correct
3. ClientId matches the Keycloak client configuration
4. Token hasn't expired

### Invalid Token Errors

Verify:
1. Token was issued by the configured Keycloak realm
2. Token includes the correct audience (client ID)
3. Token signature is valid (validated automatically against Keycloak's public keys)
