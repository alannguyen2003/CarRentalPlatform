using BuildObject;
using BuildObject.Entities;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DataAccessLayer;

public class ApplicationDbContext : DbContext
{
    private static string ConnectionString = "DefaultConnectionString";
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<AccountEntity>? Accounts { get; set; } 
    public DbSet<LocationEntity> Locations { get; set; }
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<BookingEntity> Bookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var stringConnection = configuration.GetConnectionString(ConnectionString);
        return stringConnection;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}