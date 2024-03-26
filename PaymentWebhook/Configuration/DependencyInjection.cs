using DataAccess.DataAccessLayer;
using Repository.Repository;
using Repository.Repository.Abstract;

namespace PaymentWebhook.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependence(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ApplicationDbContext>();
        return services;
    }
    
}