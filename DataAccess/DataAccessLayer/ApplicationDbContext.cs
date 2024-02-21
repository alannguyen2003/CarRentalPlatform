using BuildObject;
using BuildObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccessLayer;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<RoleEntity>? Roles { get; set; }
    public DbSet<AccountEntity>? Accounts { get; set; } 
    public DbSet<DriverLicenseEntity> DriverLicenses { get; set; }
    public DbSet<LocationEntity> Locations { get; set; }
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<ContractEntity> Contracts { get; set; }
    public DbSet<BookingEntity> Bookings { get; set; }
}