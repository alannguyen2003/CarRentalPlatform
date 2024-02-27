namespace CarRentalPlatform.Configuration.Enum;

public static class JsonConfiguration
{
    public static string ConnectionString = "DefaultConnectionString";
    public static string AdminEmail = "AdminAccount:Email";
    public static string AdminPassword = "AdminAccount:Password";

    public static string? GetValueFromAppSettings(string key)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        return configuration.GetSection(key).Value;  
    }
}