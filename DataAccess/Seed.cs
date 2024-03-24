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
using BusinessObject.Entities;

namespace DataAccess
{
    public class Seed
    {
        public static async Task SeedAccount(ApplicationDbContext context)
        {
            if (!await context.Accounts.AnyAsync())
            {
                var accounts = new List<AccountEntity>
            {
                new AccountEntity { Email = "admin@gmail.com", FirstName = "Admin", LastName = "Nguyen", PhoneNumber = "0847919292", Gender = true, Password = "1", WalletBalance = 2000000, DriverLicense = "103103103103", Role = 1 },
                new AccountEntity { Email = "employee1@gmail.com", FirstName = "Tran Minh", LastName = "Quoc", PhoneNumber = "0913243528", Gender = false, Password = "1", WalletBalance = 6000000, DriverLicense = "234567890123", Role = 2 },
                new AccountEntity { Email = "employee2@gmail.com", FirstName = "Ho Duong", LastName = "Trung Nguyen", PhoneNumber = "0386392391", Gender = true, Password = "1", WalletBalance = 820000, DriverLicense = "345678901234", Role = 2 },
                new AccountEntity { Email = "customer1@gmail.com", FirstName = "Nguyen", LastName = "Phuong Kiet", PhoneNumber = "0913823109", Gender = false, Password = "1", WalletBalance = 9500000, DriverLicense = "456789012345", Role = 3 },
                new AccountEntity { Email = "customer2@gmail.com", FirstName = "Vo", LastName = "Son Nghi", PhoneNumber = "0934192391", Gender = true, Password = "1", WalletBalance = 8300000, DriverLicense = "567890123456", Role = 3 },
                new AccountEntity { Email = "customer3@gmail.com", FirstName = "Vo", LastName = "Tan Tai", PhoneNumber = "0941923841", Gender = true, Password = "1", WalletBalance = 923000, DriverLicense = "567890123456", Role = 3 }
            };


                foreach (var account in accounts)
                {
                    await context.Accounts.AddAsync(account);
                }

                await context.SaveChangesAsync();
            }


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
                    new LocationEntity { Address = "Lô E2a-7, Đường D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh" },
                    new LocationEntity { Address = "Nhà Văn Hóa Sinh Viên, Lưu Hữu Phước, Đông Hoà, Dĩ An, Bình Dương" }
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
                    new CarEntity { Model = "Camry", LicensePlate = "59C84133", ThumbnailImage = "images/car-1.jpg", PricePerDay = 1000000, PricePerHour = 100000, PricePerMonth = 28000000, Status = 1, Description = "Comfortable mid-size car", BrandId = brands[0].Id, LocationId = locations[0].Id },
                    new CarEntity { Model = "F-150", LicensePlate = "59A91324", ThumbnailImage = "images/car-2.jpg", PricePerDay = 750000, PricePerHour = 80000, PricePerMonth = 20000000, Status = 1, Description = "Robust pickup truck", BrandId = brands[1].Id, LocationId = locations[1].Id },
                    new CarEntity { Model = "Civic", LicensePlate = "86F38129", ThumbnailImage = "images/car-3.jpg", PricePerDay = 800000, PricePerHour = 90000, PricePerMonth = 23000000, Status = 1, Description = "Efficient compact car", BrandId = brands[2].Id, LocationId = locations[0].Id },
                    new CarEntity { Model = "Impala", LicensePlate = "72A60740", ThumbnailImage = "images/car-4.jpg", PricePerDay = 900000, PricePerHour = 95000, PricePerMonth = 25000000, Status = 1, Description = "Spacious sedan", BrandId = brands[3].Id, LocationId = locations[1].Id },
                    new CarEntity { Model = "Altima", LicensePlate = "72C83721", ThumbnailImage = "images/car-5.jpg", PricePerDay = 500000, PricePerHour = 93000, PricePerMonth = 15000000, Status = 1, Description = "Reliable family car", BrandId = brands[4].Id, LocationId = locations[0].Id },
                    new CarEntity { Model = "Tacoma", LicensePlate = "51A29034", ThumbnailImage = "images/car-6.jpg", PricePerDay = 1200000, PricePerHour = 120000, PricePerMonth = 35000000, Status = 1, Description = "Durable pickup truck", BrandId = brands[0].Id, LocationId = locations[0].Id }
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
                new BookingEntity { StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5), ActualReturnDate = DateTime.Now.AddDays(-5), Feedback = "Very good service", Note = "No issues", DepositAmount = 2500000, TotalAmount = 5000000, CustomerId = accounts[3].Id, CarId = cars[0].Id, Status = 5 },
                new BookingEntity { StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddDays(-15), ActualReturnDate = DateTime.Now.AddDays(-14), Feedback = "Excellent car", Note = "Minor scratches", DepositAmount = 2250000, TotalAmount = 4500000, CustomerId = accounts[4].Id, CarId = cars[3].Id, Status = 5 },
                new BookingEntity { StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(-25), ActualReturnDate = DateTime.Now.AddDays(-24), Feedback = "Okay experience", Note = "Late return", DepositAmount = 2000000, TotalAmount = 4000000, CustomerId = accounts[3].Id, CarId = cars[2].Id, Status = 4 },
                new BookingEntity { StartDate = DateTime.Now.AddDays(-15), EndDate = DateTime.Now.AddDays(-10), ActualReturnDate = DateTime.Now.AddDays(-9), Feedback = "Great car, will rent again", Note = "All good", DepositAmount = 1875000, TotalAmount = 3750000, CustomerId = accounts[4].Id, CarId = cars[1].Id, Status = 5 },
                new BookingEntity { StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddDays(-1), ActualReturnDate = DateTime.Now, Feedback = "Car was not clean", Note = "Dirty interior", DepositAmount = 1000000, TotalAmount = 2000000, CustomerId = accounts[3].Id, CarId = cars[4].Id, Status = 4 }
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
