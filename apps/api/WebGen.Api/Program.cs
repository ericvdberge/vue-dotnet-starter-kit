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

string[] _keys = ["Key1", "Key2", "Key3"];
app.MapGet("/secure", () => TypedResults.Ok(_keys))
    .Produces<string[]>(StatusCodes.Status200OK)
    .RequireAuthorization();

await app.RunAsync();