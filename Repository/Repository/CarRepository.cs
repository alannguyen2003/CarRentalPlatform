using BuildObject.Entities;
using BusinessObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.DataTransfer.Response;
using DataTransferLayer.Page;
using Microsoft.EntityFrameworkCore;
using Repository.Repository.Utils;

namespace Repository.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDAO _carDao;

        public CarRepository()
        {
            _carDao = new CarDAO();
        }
        public Task CreateCar(CarEntity entity) => _carDao.Create(entity);

        public Task<CarEntity?> GetCarById(int? id) => _carDao.GetCarsByIdAsync(id);
        public Task<CarResponse?> GetCarResponseById(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CarEntity>> GetAllCars() => _carDao.GetAll().Result.ToListAsync();
        public async Task<List<CarResponse>> GetAllCarResponses()
        {
            var cars =  _carDao.GetCarsAsync().Result;
            List<CarResponse> listResponse = new List<CarResponse>();
            foreach (var item in cars)
            {
                CarResponse carResponse = new CarResponse()
                {
                    Id = item.Id,
                    Model = item.Model,
                    LicensePlate = item.LicensePlate,
                    PricePerDay = item.PricePerDay,
                    Description = item.Description,
                    ThumbnailImage = item.ThumbnailImage,
                    Location = item.Location.Address,
                    Brand = item.Brand.BrandName,
                    Status = ConvertUtilization.GetCarStatus(item.Status)
                };
                listResponse.Add(carResponse);
            }
            return listResponse;
        }

        public async Task<CarCategoryPage> GetDataCarCategoryPage()
        {
            var cars = _carDao.GetCarsAsync().Result;
            var carCategoryPage = new CarCategoryPage()
            {
                CurrentPage = 1
            };
            foreach (var item in cars)
            {
                var car = new CarDto()
                {
                    Id = item.Id,
                    Model = item.Model,
                    LicensePlate = item.LicensePlate,
                    Description = item.Description,
                    ThumbnailImage = item.ThumbnailImage,
                    PricePerDay = item.PricePerDay,
                    PricePerHour = item.PricePerHour,
                    PricePerMonth = item.PricePerMonth,
                    Brand = item.Brand.BrandName,
                    Location = item.Location.Address
                };
                carCategoryPage.Cars.Add(car);
            }
            return carCategoryPage;
        }

        public async Task<CarDto?> GetCarByIdDto(int? id)
        {
            var car = _carDao.GetCarsByIdAsync(id).Result;
            var carDto = new CarDto()
            {
                Id = car.Id,
                Model = car.Model,
                LicensePlate = car.LicensePlate,
                Description = car.Description,
                ThumbnailImage = car.ThumbnailImage,
                PricePerDay = car.PricePerDay,
                PricePerHour = car.PricePerHour,
                PricePerMonth = car.PricePerMonth,
                Brand = car.Brand.BrandName,
                Location = car.Location.Address
            };
            return carDto;
        }

        public Task UpdateCar(CarEntity entity) => _carDao.UpdateEntity(entity);

        public Task DeleteCar(CarEntity entity) => _carDao.DeleteEntity(entity);
        public Task<List<CarEntity>> GetPaginatedResult(int currentPage) => _carDao.GetPaginatedResult(currentPage);
    }
}
