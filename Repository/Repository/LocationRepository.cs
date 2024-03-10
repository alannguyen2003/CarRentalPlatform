using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly LocationDAO _locationDao;

        public LocationRepository()
        {
            _locationDao = new LocationDAO();
        }
        public Task CreateLocation(LocationEntity entity) => _locationDao.Create(entity);

        public async Task<List<LocationEntity>> GetAllLocations() => await _locationDao.GetAll().Result.ToListAsync();   

        public Task<LocationEntity?> GetLocationById(int id) => _locationDao.GetEntityById(id);

        public Task UpdateLocation(LocationEntity entity) => _locationDao.UpdateEntity(entity);

        public Task DeleteLocation(LocationEntity entity) => _locationDao.DeleteEntity(entity);
    }
}
