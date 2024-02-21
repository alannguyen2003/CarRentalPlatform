using CarRentalPlatform.Configuration.Enum;
using DataAccess.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPlatform.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(JsonConfiguration.ConnectionString),
                    optionBuilders =>
                        optionBuilders.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)),
                ServiceLifetime.Transient
        );
        services.AddScoped<ApplicationDbContext>();
        return services;
    }
    
}