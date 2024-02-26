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

    public class CarEntityDAO : BaseDao<CarEntity>
    {
        private readonly ApplicationDbContext _context;

        public CarEntityDAO()
        {
            _context = new ApplicationDbContext();
        }
        public async Task<IList<CarEntity>> GetCarsAsync()
        {
            try
            {
                return await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Location).ToListAsync(); 
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}" + ex);
            }
        }
        public async Task<CarEntity> GetCarsByIdAsync(int? id)
        {
            try
            {
                return await _context.Cars
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
