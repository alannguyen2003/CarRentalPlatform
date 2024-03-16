using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Seed
    {
        public static async Task SeedAccount(ApplicationDbContext context)
        {
            if (await context.Accounts.AnyAsync()) { return; }

            var accounts = new List<AccountEntity>
            {
                new AccountEntity { Email = "a1@gmail.com", FirstName = "Admin1", LastName = "Seed", PhoneNumber = "0214567894", Gender = true, Password = "1", WalletBalance = 100, DriverLicense = "103103103103", Role = 1 },
                new AccountEntity { Email = "a2@gmail.com", FirstName = "Admin2", LastName = "Seed", PhoneNumber = "2345678901", Gender = false, Password = "1", WalletBalance = 600, DriverLicense = "234567890123", Role = 2 },
                new AccountEntity { Email = "e1@gmail.com", FirstName = "Employ1", LastName = "Seed", PhoneNumber = "3456789012", Gender = true, Password = "1", WalletBalance = 700, DriverLicense = "345678901234", Role = 2 },
                new AccountEntity { Email = "c1@gmail.com", FirstName = "Cus1", LastName = "Seed", PhoneNumber = "4567890123", Gender = false, Password = "1", WalletBalance = 800, DriverLicense = "456789012345", Role = 3 },
                new AccountEntity { Email = "c2@gmail.com", FirstName = "Cus2", LastName = "Seed", PhoneNumber = "5678901234", Gender = true, Password = "1", WalletBalance = 900, DriverLicense = "567890123456", Role = 3 }

            };


            foreach (var account in accounts)
            {
                await context.Accounts.AddAsync(account);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedBrand(ApplicationDbContext context)
        {
            if (!await context.Brands.AnyAsync())
            {
                var brands = new List<BrandEntity>
                {
                    new BrandEntity { BrandName = "Toyota" },
                    new BrandEntity { BrandName = "Ford" },
                    new BrandEntity { BrandName = "Honda" },
                    new BrandEntity { BrandName = "Chevrolet" },
                    new BrandEntity { BrandName = "Nissan" },
                    new BrandEntity { BrandName = "VinFast" }
                };

                foreach (var brand in brands)
                {
                    await context.Brands.AddAsync(brand);
                }
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedLocation(ApplicationDbContext context)
        {
            if (!await context.Locations.AnyAsync())
            {
                var locations = new List<LocationEntity>
                {
                    new LocationEntity { Address = "123 Main St, City, Country" },
                    new LocationEntity { Address = "456 Elm St, City, Country" },
                    new LocationEntity { Address = "789 Pine St, City, Country" },
                    new LocationEntity { Address = "101 Maple St, City, Country" },
                    new LocationEntity { Address = "202 Oak St, City, Country" }
                };

                foreach (var location in locations)
                {
                    await context.Locations.AddAsync(location);
                }
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedCar(ApplicationDbContext context)
        {
            if (!await context.Cars.AnyAsync())
            {
                // Ensure brands and locations are seeded first
                await SeedBrand(context);
                await SeedLocation(context);

                var brands = await context.Brands.ToListAsync();
                var locations = await context.Locations.ToListAsync();

                var cars = new List<CarEntity>
                {
                    new CarEntity { Model = "Camry", LicensePlate = "ABC123", ThumbnailImage = "images/car-1.jpg", PricePerDay = 50, PricePerHour = 5, PricePerMonth = 1000, Status = 1, Description = "Comfortable mid-size car", BrandId = brands[0].Id, LocationId = locations[0].Id },
                    new CarEntity { Model = "F-150", LicensePlate = "DEF456", ThumbnailImage = "images/car-2.jpg", PricePerDay = 70, PricePerHour = 7, PricePerMonth = 1400, Status = 1, Description = "Robust pickup truck", BrandId = brands[1].Id, LocationId = locations[1].Id },
                    new CarEntity { Model = "Civic", LicensePlate = "GHI789", ThumbnailImage = "images/car-3.jpg", PricePerDay = 40, PricePerHour = 4, PricePerMonth = 800, Status = 1, Description = "Efficient compact car", BrandId = brands[2].Id, LocationId = locations[2].Id },
                    new CarEntity { Model = "Impala", LicensePlate = "JKL012", ThumbnailImage = "images/car-4.jpg", PricePerDay = 60, PricePerHour = 6, PricePerMonth = 1200, Status = 1, Description = "Spacious sedan", BrandId = brands[3].Id, LocationId = locations[3].Id },
                    new CarEntity { Model = "Altima", LicensePlate = "MNO345", ThumbnailImage = "images/car-5.jpg", PricePerDay = 55, PricePerHour = 5, PricePerMonth = 1100, Status = 1, Description = "Reliable family car", BrandId = brands[4].Id, LocationId = locations[4].Id },
                    new CarEntity { Model = "Tacoma", LicensePlate = "PQR678", ThumbnailImage = "images/car-6.jpg", PricePerDay = 80, PricePerHour = 8, PricePerMonth = 1600, Status = 1, Description = "Durable pickup truck", BrandId = brands[0].Id, LocationId = locations[0].Id }
                };

                foreach (var car in cars)
                {
                    await context.Cars.AddAsync(car);
                }
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedBooking(ApplicationDbContext context)
        {
            if (!await context.Bookings.AnyAsync())
            {
                var cars = await context.Cars.ToListAsync();
                var accounts = await context.Accounts.ToListAsync();

                var bookings = new List<BookingEntity>
            {
                new BookingEntity { StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5), ActualReturnDate = DateTime.Now.AddDays(-5), Feedback = "Very good service", Note = "No issues", DepositAmount = 100, TotalAmount = 500, CustomerId = accounts[3].Id, CarId = cars[0].Id, Status = 4},
                new BookingEntity { StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddDays(-15), ActualReturnDate = DateTime.Now.AddDays(-14), Feedback = "Excellent car", Note = "Minor scratches", DepositAmount = 200, TotalAmount = 600, CustomerId = accounts[4].Id, CarId = cars[3].Id, Status = 4 },
                new BookingEntity { StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(-25), ActualReturnDate = DateTime.Now.AddDays(-24), Feedback = "Okay experience", Note = "Late return", DepositAmount = 150, TotalAmount = 550, CustomerId = accounts[3].Id, CarId = cars[2].Id, Status = 4 },
                new BookingEntity { StartDate = DateTime.Now.AddDays(-15), EndDate = DateTime.Now.AddDays(-10), ActualReturnDate = DateTime.Now.AddDays(-9), Feedback = "Great car, will rent again", Note = "All good", DepositAmount = 100, TotalAmount = 500, CustomerId = accounts[4].Id, CarId = cars[1].Id, Status = 4 },
                new BookingEntity { StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddDays(-1), ActualReturnDate = DateTime.Now, Feedback = "Car was not clean", Note = "Dirty interior", DepositAmount = 120, TotalAmount = 520, CustomerId = accounts[3].Id, CarId = cars[4].Id, Status = 4 }
            };

                foreach (var booking in bookings)
                {
                    await context.Bookings.AddAsync(booking);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
