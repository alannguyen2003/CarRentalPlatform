using CarRentalPlatform.Configuration.Enum;
using DataAccess.DataAccessLayer;
using Repository.Repository;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependence(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<ApplicationDbContext>();
        return services;
    }
    
}