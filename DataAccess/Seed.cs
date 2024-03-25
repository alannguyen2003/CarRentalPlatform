using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
                new AccountEntity { Email = "admin@gmail.com", FirstName = "Admin", LastName = "Nguyen", PhoneNumber = "0847919292", Gender = true, Password = "123", WalletBalance = 2000000, DriverLicense = "103103103103", Role = 1 },
                new AccountEntity { Email = "employee1@gmail.com", FirstName = "Tran Minh", LastName = "Quoc", PhoneNumber = "0913243528", Gender = true, Password = "123", WalletBalance = 6000000, DriverLicense = "234567890123", Role = 2 },
                new AccountEntity { Email = "employee2@gmail.com", FirstName = "Ho Duong", LastName = "Trung Nguyen", PhoneNumber = "0386392391", Gender = true, Password = "123", WalletBalance = 820000, DriverLicense = "345678901234", Role = 2 },
                new AccountEntity { Email = "customer1@gmail.com", FirstName = "Nguyen", LastName = "Phuong Kiet", PhoneNumber = "0913823109", Gender = true, Password = "123", WalletBalance = 9500000, DriverLicense = "456789012345", Role = 3 },
                new AccountEntity { Email = "customer2@gmail.com", FirstName = "Vo", LastName = "Son Nghi", PhoneNumber = "0934192391", Gender = true, Password = "123", WalletBalance = 8300000, DriverLicense = "567890123456", Role = 3 },
                new AccountEntity { Email = "customer3@gmail.com", FirstName = "Vo", LastName = "Tan Tai", PhoneNumber = "0941923841", Gender = true, Password = "123", WalletBalance = 923000, DriverLicense = "550367220001", Role = 3 },
                new AccountEntity { Email = "customer4@gmail.com", FirstName = "Pham Tuan", LastName = "Cao Thang", PhoneNumber = "0941923841", Gender = true, Password = "123", WalletBalance = 1000, DriverLicense = "550392231440", Role = 3 }
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
                    new BrandEntity { BrandName = "VinFast" },
                    new BrandEntity { BrandName = "BMW" },
                    new BrandEntity { BrandName = "Mercedes-Benz" },
                    new BrandEntity { BrandName = "Audi" },
                    new BrandEntity { BrandName = "Tesla" },
                    new BrandEntity { BrandName = "Hyundai" },
                    new BrandEntity { BrandName = "Kia" },
                    new BrandEntity { BrandName = "Lamborghini" },
                    new BrandEntity { BrandName = "Ferrari" },
                    new BrandEntity { BrandName = "Porsche" },
                    new BrandEntity { BrandName = "Audi" },
                    new BrandEntity { BrandName = "McLaren" },
                    new BrandEntity { BrandName = "Aston Martin" },
                    new BrandEntity { BrandName = "Rolls-Royce" },
                    new BrandEntity { BrandName = "Bugatti" },
                    new BrandEntity { BrandName = "Bentley" }
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
                    new CarEntity { Model = "Camry", LicensePlate = "59F42053", ThumbnailImage = "images/car-toyota-camry.jpg", PricePerDay = 1000000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Comfortable mid-size car", BrandId = brands.First(b => b.BrandName == "Toyota").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "F-150", LicensePlate = "51A72817", ThumbnailImage = "images/car-ford-f150.jpg", PricePerDay = 950000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Robust pickup truck", BrandId = brands.First(b => b.BrandName == "Ford").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Civic", LicensePlate = "60G12702", ThumbnailImage = "images/car-honda-civic.jpg", PricePerDay = 920000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Efficient compact car", BrandId = brands.First(b => b.BrandName == "Honda").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Impala", LicensePlate = "59C87859", ThumbnailImage = "images/car-chevrolet-impala.jpg", PricePerDay = 800000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Spacious sedan", BrandId = brands.First(b => b.BrandName == "Chevrolet").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Altima", LicensePlate = "51B61979", ThumbnailImage = "images/car-nissan-altima.jpg", PricePerDay = 840000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Reliable family car", BrandId = brands.First(b => b.BrandName == "Nissan").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "VFe36", LicensePlate = "60F45584", ThumbnailImage = "images/car-vinfast-vfe36.jpg", PricePerDay = 900000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Electricity family car", BrandId = brands.First(b => b.BrandName == "VinFast").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Tacoma", LicensePlate = "59A80892", ThumbnailImage = "images/car-toyota-tacoma.jpg", PricePerDay = 900000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Durable pickup truck", BrandId = brands.First(b => b.BrandName == "Toyota").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Model 3", LicensePlate = "51G55936", ThumbnailImage = "images/car-tesla-model3.jpg", PricePerDay = 950000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Luxury electric sedan", BrandId = brands.First(b => b.BrandName == "Tesla").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "3 Series", LicensePlate = "60C10841", ThumbnailImage = "images/car-bmw-3series.jpg", PricePerDay = 970000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Sporty sedan with superior handling", BrandId = brands.First(b => b.BrandName == "BMW").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Santa Fe", LicensePlate = "59B41525", ThumbnailImage = "images/car-hyundai-santafe.jpg", PricePerDay = 700000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Spacious family SUV", BrandId = brands.First(b => b.BrandName == "Hyundai").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Rio", LicensePlate = "51F78229", ThumbnailImage = "images/car-kia-rio.jpg", PricePerDay = 800000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Economical compact car", BrandId = brands.First(b => b.BrandName == "Kia").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "GLC", LicensePlate = "60A42071", ThumbnailImage = "images/car-mercedes-glc.jpg", PricePerDay = 750000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Luxury midsize SUV", BrandId = brands.First(b => b.BrandName == "Mercedes-Benz").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "G63 AMG", LicensePlate = "59G07182", ThumbnailImage = "images/car-mercedes-g63.jpg", PricePerDay = 940000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "High-performance luxury SUV", BrandId = brands.First(b => b.BrandName == "Mercedes-Benz").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Huracan", LicensePlate = "51C44748", ThumbnailImage = "images/car-lamborghini-huracan.jpg", PricePerDay = 800000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Exotic supercar with aggressive styling", BrandId = brands.First(b => b.BrandName == "Lamborghini").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "488 Spider", LicensePlate = "60B42263", ThumbnailImage = "images/car-ferrari-488spider.jpg", PricePerDay = 870000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Iconic Italian sports car with retractable hardtop", BrandId = brands.First(b => b.BrandName == "Ferrari").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "911 Turbo", LicensePlate = "59F11700", ThumbnailImage = "images/car-porsche-911turbo.jpg", PricePerDay = 820000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "High-performance sports car with turbocharged engine", BrandId = brands.First(b => b.BrandName == "Porsche").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Aventador", LicensePlate = "51A69698", ThumbnailImage = "images/car-lamborghini-aventador.jpg", PricePerDay = 730000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Flagship supercar with V12 engine", BrandId = brands.First(b => b.BrandName == "Lamborghini").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "F8 Tributo", LicensePlate = "60G44168", ThumbnailImage = "images/car-ferrari-f8tributo.jpg", PricePerDay = 700000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "High-performance sports car with turbocharged V8", BrandId = brands.First(b => b.BrandName == "Ferrari").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Cayenne", LicensePlate = "59C79027", ThumbnailImage = "images/car-porsche-cayenne.jpg", PricePerDay = 730000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Luxury midsize SUV with sporty performance", BrandId = brands.First(b => b.BrandName == "Porsche").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "R8", LicensePlate = "51B50501", ThumbnailImage = "images/car-audi-r8.jpg", PricePerDay = 900000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Mid-engine sports car with V10 engine", BrandId = brands.First(b => b.BrandName == "Audi").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "M4", LicensePlate = "60F51551", ThumbnailImage = "images/car-bmw-m4.jpg", PricePerDay = 900000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "High-performance version of the 4 Series", BrandId = brands.First(b => b.BrandName == "BMW").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "720S", LicensePlate = "59A51616", ThumbnailImage = "images/car-mclaren-720s.jpg", PricePerDay = 1200000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Super Series sports car with twin-turbo V8 engine", BrandId = brands.First(b => b.BrandName == "McLaren").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "DB11", LicensePlate = "51G16374", ThumbnailImage = "images/car-astonmartin-db11.jpg", PricePerDay = 1100000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Grand tourer with twin-turbocharged V12 engine", BrandId = brands.First(b => b.BrandName == "Aston Martin").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Continental GT", LicensePlate = "60C96435", ThumbnailImage = "images/car-bentley-continentalgt.jpg", PricePerDay = 1050000, PricePerHour = 30000000, PricePerMonth = 9300, Status = 1, Description = "Luxury coupe with timeless design", BrandId = brands.First(b => b.BrandName == "Bentley").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Phantom", LicensePlate = "59B37330", ThumbnailImage = "images/car-rollsroyce-phantom.jpg", PricePerDay = 1030000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Epitome of luxury sedans", BrandId = brands.First(b => b.BrandName == "Rolls-Royce").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Chiron", LicensePlate = "51F06730", ThumbnailImage = "images/car-bugatti-chiron.jpg", PricePerDay = 900000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Ultimate hypercar with unparalleled performance", BrandId = brands.First(b => b.BrandName == "Bugatti").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "GT-R", LicensePlate = "60A21029", ThumbnailImage = "images/car-nissan-gtr.jpg", PricePerDay = 950000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Legendary performance sports car", BrandId = brands.First(b => b.BrandName == "Nissan").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Senna", LicensePlate = "59G58690", ThumbnailImage = "images/car-mclaren-senna.jpg", PricePerDay = 980000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Track-focused hypercar", BrandId = brands.First(b => b.BrandName == "McLaren").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Vantage", LicensePlate = "51C04416", ThumbnailImage = "images/car-astonmartin-vantage.jpg", PricePerDay = 730000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Agile and compact sports car", BrandId = brands.First(b => b.BrandName == "Aston Martin").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Urus", LicensePlate = "60B44869", ThumbnailImage = "images/car-lamborghini-urus.jpg", PricePerDay = 820000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "The world's first Super Sport Utility Vehicle", BrandId = brands.First(b => b.BrandName == "Lamborghini").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "488 Pista", LicensePlate = "59F16194", ThumbnailImage = "images/car-ferrari-488pista.jpg", PricePerDay = 900000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Race-inspired supercar with extreme performance", BrandId = brands.First(b => b.BrandName == "Ferrari").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "911 GT3 RS", LicensePlate = "51A85839", ThumbnailImage = "images/car-porsche-911gt3rs.jpg", PricePerDay = 830000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "High-performance sports car with a focus on track ability", BrandId = brands.First(b => b.BrandName == "Porsche").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Supra", LicensePlate = "60G41211", ThumbnailImage = "images/car-toyota-supra.jpg", PricePerDay = 900000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Iconic sports car reborn", BrandId = brands.First(b => b.BrandName == "Toyota").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Cullinan", LicensePlate = "59C19081", ThumbnailImage = "images/car-rollsroyce-cullinan.jpg", PricePerDay = 920000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "Luxury SUV offering unmatched off-road capabilities", BrandId = brands.First(b => b.BrandName == "Rolls-Royce").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Mulsanne", LicensePlate = "51B16323", ThumbnailImage = "images/car-bentley-mulsanne.jpg", PricePerDay = 710000, PricePerHour = 100000, PricePerMonth = 30000000, Status = 1, Description = "A pinnacle of luxury and performance", BrandId = brands.First(b => b.BrandName == "Bentley").Id, LocationId = locations[new Random().Next(locations.Count)].Id },

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
