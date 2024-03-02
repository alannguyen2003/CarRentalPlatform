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

    public class CarDAO : BaseDao<CarEntity>
    {
        private static CarDAO instance = null;
        private static readonly object instanceLock = new object();
        private CarDAO() { }
        public static CarDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CarDAO();
                    }
                }
                return instance;
            }
        }
        public async Task<IList<CarEntity>> GetCarsAsync()
        {
            try
            {
                var _dbContext = new ApplicationDbContext();
                return await _dbContext.Cars
                .Include(c => c.Brand)
                .Include(c => c.Location).ToListAsync(); 
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}" + ex);
            }
        }
        public async Task<CarEntity?> GetCarsByIdAsync(int? id)
        {
            try
            {
                var _dbContext = new ApplicationDbContext();
                return await _dbContext.Cars
                .Include(c => c.Brand)
                .Include(c => c.Location)
                .Where(i => i.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}" + ex);
            }
        }
    }
}
