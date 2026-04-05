using WebGen.Api.Authentication;
using WebGen.Api.Cors;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPermissiveCors()
    .AddKeycloakAuthentication(builder.Configuration)
    .AddAuthorization()
    .AddOpenApi();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();
//app.UseHttpsRedirection();

// Public endpoint
app.MapGet("/", () => "API running");

// Protected endpoint - requires Keycloak token
app.MapGet("/secure", () => "You are authenticated!")
    .RequireAuthorization();

app.Run();