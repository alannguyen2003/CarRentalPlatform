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
        //services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ApplicationDbContext>();
        return services;
    }

    public static IServiceCollection AddLoggingInformation(this IServiceCollection services)
    {
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.Response;
            logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.Request;
            logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseBody;
            logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestBody;
            logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPath;
        });
        return services;
    }
    
}