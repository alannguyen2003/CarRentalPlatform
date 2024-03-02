using BuildObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Abstract
{
    public interface ICarRepository
    {
        Task CreateCar(CarEntity entity);
        Task<CarEntity?> GetCarById(int? id);
        Task<IList<CarEntity>> GetAllCars();
        Task UpdateCar(CarEntity entity);
        Task DeleteCar(CarEntity entity);
    }
}
