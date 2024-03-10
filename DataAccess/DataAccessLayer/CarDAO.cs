using BuildObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.DataAccessLayer
{

    public class CarDAO : BaseDao<CarEntity>
    {
        private readonly ApplicationDbContext _context;

        public CarDAO() => _context = new ApplicationDbContext();
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
        public async Task<CarEntity?> GetCarsByIdAsync(int? id)
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
