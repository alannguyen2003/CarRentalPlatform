using CarRentalPlatform.Configuration.Enum;
using DataAccess.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPlatform.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();
        return services;
    }
    
}