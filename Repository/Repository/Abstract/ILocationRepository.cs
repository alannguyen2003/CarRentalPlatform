using BuildObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Abstract
{
    public interface ILocationRepository
    {
        Task CreateLocation(LocationEntity entity);
        Task<LocationEntity?> GetLocationById(int id);
        Task<List<LocationEntity>> GetAllLocations();
        Task UpdateLocation(LocationEntity entity);
        Task DeleteLocation(LocationEntity entity);
    }
}
