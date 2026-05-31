using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebGen.Infrastructure.Repositories;

namespace Webgen.Infrastructure;

public static class InfraServiceExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContextPool<AppDbContext>(opt => {
                opt.UseNpgsql(
                   configuration.GetConnectionString("AppDbContext"),
                   o => o.SetPostgresVersion(13, 0));

                opt.UseSnakeCaseNamingConvention();
                
            })
            .AddScoped<IUnitOfWork, UnitOfWork>();

        services
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
