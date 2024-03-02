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
    public class LocationRepository : ILocationRepository
    {
        public Task CreateLocation(LocationEntity entity) => LocationDAO.Instance.Create(entity);

        public Task<IList<LocationEntity>> GetAllLocations() => LocationDAO.Instance.GetLocationsAsync();   

        public Task<LocationEntity?> GetLocationById(int id) => LocationDAO.Instance.GetEntityById(id);

        public Task UpdateLocation(LocationEntity entity) => LocationDAO.Instance.UpdateEntity(entity);

        public Task DeleteLocation(LocationEntity entity) => LocationDAO.Instance.DeleteEntity(entity);
    }
}
