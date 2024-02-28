using BuildObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessLayer
{
    public class LocationDAO : BaseDao<LocationEntity>
    {
        private static LocationDAO instance = null;
        private static readonly object instanceLock = new object();
        private LocationDAO() { }
        public static LocationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LocationDAO();
                    }
                }
                return instance;
            }
        }

        public async Task<IList<LocationEntity>> GetLocationsAsync()
        {
            try
            {
                var _dbContext = new ApplicationDbContext();
                return await _dbContext.Locations.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}" + ex);
            }
        }
    }
}
