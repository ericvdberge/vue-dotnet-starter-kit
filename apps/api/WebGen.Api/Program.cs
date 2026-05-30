using Webgen.Infrastructure;
using WebGen.Application;
using WebGen.Api.Authentication;
using WebGen.Api.Cors;
using Scalar.AspNetCore;
using WebGen.Api;

var builder = WebApplication.CreateBuilder(args);

///<summary>
/// Authentication and services first
/// </summary>
builder.Services
    .AddSecureCors(builder.Configuration)
    .AddKeycloakAuthentication(builder.Configuration)
    .AddAuthorization()
    .AddOpenApi();

///<summary>
/// App Services after authentication
/// </summary>
builder.Services
    .AddApi()
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();
app.UseCors(CorsExtensions.SECURE_CORS);
app.UseAuthentication();
app.UseAuthorization();
app.MapOpenApi();
app.MapScalarApiReference("/docs/");

#if !DEBUG
//app.UseHttpsRedirection();
#endif

// Public endpoint
app.MapGet("/", () => "API running");
app.MapApi();

string[] _keys = ["Key1", "Key2", "Key3"];
app.MapGet("/secure", () => TypedResults.Ok(_keys))
    .Produces<string[]>(StatusCodes.Status200OK)
    .RequireAuthorization();

await app.RunAsync();