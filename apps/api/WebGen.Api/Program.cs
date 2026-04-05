using WebGen.Api.Authentication;
using WebGen.Api.Cors;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSecureCors(builder.Configuration)
    .AddKeycloakAuthentication(builder.Configuration)
    .AddAuthorization()
    .AddOpenApi();

var app = builder.Build();

app.UseCors("SecureUrls");
app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();

#if !DEBUG
//app.UseHttpsRedirection();
#endif

// Public endpoint
app.MapGet("/", () => "API running");

// Protected endpoint - requires Keycloak token
app.MapGet("/secure", () => "You are authenticated!")
    .RequireAuthorization();

await app.RunAsync();