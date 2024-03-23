using BuildObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.DataTransfer.Response;
using DataTransferLayer.Page;

namespace Repository.Repository.Abstract
{
    public interface ICarRepository
    {
        Task CreateCar(CarEntity entity);
        Task<CarDto?> GetCarByIdDto(int? id);
        Task<CarEntity?> GetCarById(int? id);
        Task<CarResponse?> GetCarResponseById(int? id);
        Task<List<CarEntity>> GetAllCars();
        Task<List<CarResponse>> GetAllCarResponses();
        Task<CarCategoryPage> GetDataCarCategoryPage();
        Task UpdateCar(CarEntity entity);
        Task DeleteCar(CarEntity entity);
        Task<List<CarEntity>> GetPaginatedResult(int currentPage);
        Task<List<CarEntity>> FilterCarsByBrandAsync(string? brandName);
        Task<List<CarEntity>> FilterCarsByLocationAsync(string? location);
        Task<List<CarEntity>> FilterCarsByModelAsync(string? model);
        Task<List<CarEntity>> SearchCarsAsync(string? searchTerm);
    }
}
