using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CarRepository : ICarRepository
    {
        public Task CreateCar(CarEntity entity) => CarDAO.Instance.Create(entity);

        public Task<IList<CarEntity>> GetAllCars() => CarDAO.Instance.GetCarsAsync();

        public Task<CarEntity?> GetCarById(int? id) => CarDAO.Instance.GetCarsByIdAsync(id);

        public Task UpdateCar(CarEntity entity) => CarDAO.Instance.UpdateEntity(entity);

        public Task DeleteCar(CarEntity entity) => CarDAO.Instance.DeleteEntity(entity);
    }
}
