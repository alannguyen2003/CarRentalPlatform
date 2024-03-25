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
                    new BrandEntity { BrandName = "Bentley" },



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
                    new LocationEntity { Address = "789 Pine St, City, Country" }
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
                    new CarEntity { Model = "Camry", LicensePlate = "ABC123", ThumbnailImage = "images/car-toyota-camry.jpg", PricePerDay = 50, PricePerHour = 5, PricePerMonth = 1000, Status = 1, Description = "Comfortable mid-size car", BrandId = brands.First(b => b.BrandName == "Toyota").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "F-150", LicensePlate = "DEF456", ThumbnailImage = "images/car-ford-f150.jpg", PricePerDay = 70, PricePerHour = 7, PricePerMonth = 1400, Status = 1, Description = "Robust pickup truck", BrandId = brands.First(b => b.BrandName == "Ford").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Civic", LicensePlate = "GHI789", ThumbnailImage = "images/car-honda-civic.jpg", PricePerDay = 40, PricePerHour = 4, PricePerMonth = 800, Status = 1, Description = "Efficient compact car", BrandId = brands.First(b => b.BrandName == "Honda").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Impala", LicensePlate = "JKL012", ThumbnailImage = "images/car-chevrolet-impala.jpg", PricePerDay = 60, PricePerHour = 6, PricePerMonth = 1200, Status = 1, Description = "Spacious sedan", BrandId = brands.First(b => b.BrandName == "Chevrolet").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Altima", LicensePlate = "MNO345", ThumbnailImage = "images/car-nissan-altima.jpg", PricePerDay = 55, PricePerHour = 5, PricePerMonth = 1100, Status = 1, Description = "Reliable family car", BrandId = brands.First(b => b.BrandName == "Nissan").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "VFe36", LicensePlate = "VN1712", ThumbnailImage = "images/car-vinfast-vfe36.jpg", PricePerDay = 70, PricePerHour = 7, PricePerMonth = 1700, Status = 1, Description = "Electricity family car", BrandId = brands.First(b => b.BrandName == "VinFast").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Tacoma", LicensePlate = "PQR678", ThumbnailImage = "images/car-toyota-tacoma.jpg", PricePerDay = 80, PricePerHour = 8, PricePerMonth = 1600, Status = 1, Description = "Durable pickup truck", BrandId = brands.First(b => b.BrandName == "Toyota").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Model 3", LicensePlate = "TES3001", ThumbnailImage = "images/car-tesla-model3.jpg", PricePerDay = 100, PricePerHour = 10, PricePerMonth = 3000, Status = 1, Description = "Luxury electric sedan", BrandId = brands.First(b => b.BrandName == "Tesla").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "3 Series", LicensePlate = "BMW3201", ThumbnailImage = "images/car-bmw-3series.jpg", PricePerDay = 90, PricePerHour = 9, PricePerMonth = 2700, Status = 1, Description = "Sporty sedan with superior handling", BrandId = brands.First(b => b.BrandName == "BMW").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Santa Fe", LicensePlate = "HYD5012", ThumbnailImage = "images/car-hyundai-santafe.jpg", PricePerDay = 80, PricePerHour = 8, PricePerMonth = 2400, Status = 1, Description = "Spacious family SUV", BrandId = brands.First(b => b.BrandName == "Hyundai").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Rio", LicensePlate = "KIA4003", ThumbnailImage = "images/car-kia-rio.jpg", PricePerDay = 50, PricePerHour = 5, PricePerMonth = 1500, Status = 1, Description = "Economical compact car", BrandId = brands.First(b => b.BrandName == "Kia").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "GLC", LicensePlate = "MBZ5004", ThumbnailImage = "images/car-mercedes-glc.jpg", PricePerDay = 110, PricePerHour = 11, PricePerMonth = 3300, Status = 1, Description = "Luxury midsize SUV", BrandId = brands.First(b => b.BrandName == "Mercedes-Benz").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "G63 AMG", LicensePlate = "MBG6301", ThumbnailImage = "images/car-mercedes-g63.jpg", PricePerDay = 200, PricePerHour = 20, PricePerMonth = 6000, Status = 1, Description = "High-performance luxury SUV", BrandId = brands.First(b => b.BrandName == "Mercedes-Benz").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Huracan", LicensePlate = "LAM3012", ThumbnailImage = "images/car-lamborghini-huracan.jpg", PricePerDay = 250, PricePerHour = 25, PricePerMonth = 7500, Status = 1, Description = "Exotic supercar with aggressive styling", BrandId = brands.First(b => b.BrandName == "Lamborghini").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "488 Spider", LicensePlate = "FER4880", ThumbnailImage = "images/car-ferrari-488spider.jpg", PricePerDay = 300, PricePerHour = 30, PricePerMonth = 9000, Status = 1, Description = "Iconic Italian sports car with retractable hardtop", BrandId = brands.First(b => b.BrandName == "Ferrari").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "911 Turbo", LicensePlate = "POR911T", ThumbnailImage = "images/car-porsche-911turbo.jpg", PricePerDay = 220, PricePerHour = 22, PricePerMonth = 6600, Status = 1, Description = "High-performance sports car with turbocharged engine", BrandId = brands.First(b => b.BrandName == "Porsche").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Aventador", LicensePlate = "LAM4003", ThumbnailImage = "images/car-lamborghini-aventador.jpg", PricePerDay = 350, PricePerHour = 35, PricePerMonth = 10500, Status = 1, Description = "Flagship supercar with V12 engine", BrandId = brands.First(b => b.BrandName == "Lamborghini").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "F8 Tributo", LicensePlate = "FER5004", ThumbnailImage = "images/car-ferrari-f8tributo.jpg", PricePerDay = 320, PricePerHour = 32, PricePerMonth = 9600, Status = 1, Description = "High-performance sports car with turbocharged V8", BrandId = brands.First(b => b.BrandName == "Ferrari").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Cayenne", LicensePlate = "POR6005", ThumbnailImage = "images/car-porsche-cayenne.jpg", PricePerDay = 180, PricePerHour = 18, PricePerMonth = 5400, Status = 1, Description = "Luxury midsize SUV with sporty performance", BrandId = brands.First(b => b.BrandName == "Porsche").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "R8", LicensePlate = "AUD7006", ThumbnailImage = "images/car-audi-r8.jpg", PricePerDay = 230, PricePerHour = 23, PricePerMonth = 6900, Status = 1, Description = "Mid-engine sports car with V10 engine", BrandId = brands.First(b => b.BrandName == "Audi").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "M4", LicensePlate = "BMW8007", ThumbnailImage = "images/car-bmw-m4.jpg", PricePerDay = 150, PricePerHour = 15, PricePerMonth = 4500, Status = 1, Description = "High-performance version of the 4 Series", BrandId = brands.First(b => b.BrandName == "BMW").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "720S", LicensePlate = "MCL9008", ThumbnailImage = "images/car-mclaren-720s.jpg", PricePerDay = 340, PricePerHour = 34, PricePerMonth = 10200, Status = 1, Description = "Super Series sports car with twin-turbo V8 engine", BrandId = brands.First(b => b.BrandName == "McLaren").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "DB11", LicensePlate = "AST0010", ThumbnailImage = "images/car-astonmartin-db11.jpg", PricePerDay = 280, PricePerHour = 28, PricePerMonth = 8400, Status = 1, Description = "Grand tourer with twin-turbocharged V12 engine", BrandId = brands.First(b => b.BrandName == "Aston Martin").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Continental GT", LicensePlate = "BEN0020", ThumbnailImage = "images/car-bentley-continentalgt.jpg", PricePerDay = 310, PricePerHour = 31, PricePerMonth = 9300, Status = 1, Description = "Luxury coupe with timeless design", BrandId = brands.First(b => b.BrandName == "Bentley").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Phantom", LicensePlate = "RRPHAN", ThumbnailImage = "images/car-rollsroyce-phantom.jpg", PricePerDay = 400, PricePerHour = 40, PricePerMonth = 12000, Status = 1, Description = "Epitome of luxury sedans", BrandId = brands.First(b => b.BrandName == "Rolls-Royce").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Chiron", LicensePlate = "BUGCHI", ThumbnailImage = "images/car-bugatti-chiron.jpg", PricePerDay = 500, PricePerHour = 50, PricePerMonth = 15000, Status = 1, Description = "Ultimate hypercar with unparalleled performance", BrandId = brands.First(b => b.BrandName == "Bugatti").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "GT-R", LicensePlate = "NISGTR", ThumbnailImage = "images/car-nissan-gtr.jpg", PricePerDay = 220, PricePerHour = 22, PricePerMonth = 6600, Status = 1, Description = "Legendary performance sports car", BrandId = brands.First(b => b.BrandName == "Nissan").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Senna", LicensePlate = "MCLSEN", ThumbnailImage = "images/car-mclaren-senna.jpg", PricePerDay = 450, PricePerHour = 45, PricePerMonth = 13500, Status = 1, Description = "Track-focused hypercar", BrandId = brands.First(b => b.BrandName == "McLaren").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Vantage", LicensePlate = "ASTVAN", ThumbnailImage = "images/car-astonmartin-vantage.jpg", PricePerDay = 230, PricePerHour = 23, PricePerMonth = 6900, Status = 1, Description = "Agile and compact sports car", BrandId = brands.First(b => b.BrandName == "Aston Martin").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Urus", LicensePlate = "LAMURS", ThumbnailImage = "images/car-lamborghini-urus.jpg", PricePerDay = 300, PricePerHour = 30, PricePerMonth = 9000, Status = 1, Description = "The world's first Super Sport Utility Vehicle", BrandId = brands.First(b => b.BrandName == "Lamborghini").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "488 Pista", LicensePlate = "FERPST", ThumbnailImage = "images/car-ferrari-488pista.jpg", PricePerDay = 330, PricePerHour = 33, PricePerMonth = 9900, Status = 1, Description = "Race-inspired supercar with extreme performance", BrandId = brands.First(b => b.BrandName == "Ferrari").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "911 GT3 RS", LicensePlate = "PORGT3", ThumbnailImage = "images/car-porsche-911gt3rs.jpg", PricePerDay = 240, PricePerHour = 24, PricePerMonth = 7200, Status = 1, Description = "High-performance sports car with a focus on track ability", BrandId = brands.First(b => b.BrandName == "Porsche").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Supra", LicensePlate = "TOYSUP", ThumbnailImage = "images/car-toyota-supra.jpg", PricePerDay = 190, PricePerHour = 19, PricePerMonth = 5700, Status = 1, Description = "Iconic sports car reborn", BrandId = brands.First(b => b.BrandName == "Toyota").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Cullinan", LicensePlate = "RRCULL", ThumbnailImage = "images/car-rollsroyce-cullinan.jpg", PricePerDay = 400, PricePerHour = 40, PricePerMonth = 12000, Status = 1, Description = "Luxury SUV offering unmatched off-road capabilities", BrandId = brands.First(b => b.BrandName == "Rolls-Royce").Id, LocationId = locations[new Random().Next(locations.Count)].Id },
                    new CarEntity { Model = "Mulsanne", LicensePlate = "BNTMUL", ThumbnailImage = "images/car-bentley-mulsanne.jpg", PricePerDay = 350, PricePerHour = 35, PricePerMonth = 10500, Status = 1, Description = "A pinnacle of luxury and performance", BrandId = brands.First(b => b.BrandName == "Bentley").Id, LocationId = locations[new Random().Next(locations.Count)].Id },

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
