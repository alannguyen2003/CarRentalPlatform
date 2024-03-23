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
        public async Task<List<CarEntity>> GetPaginatedResult(int currentPage)
        {
            var car = await _context.Cars.ToListAsync();
            return car.Skip((currentPage-1)*12).Take(12).ToList();
        }
        public async Task<List<CarEntity>> FilterCarsByBrandAsync(string? brandName)
        {
            try
            {
                var query = _context.Cars.Include(c => c.Brand).Include(c => c.Location).AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(brandName))
                {
                    query = query.Where(c => c.Brand.BrandName == brandName);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }
        public async Task<List<CarEntity>> FilterCarsByLocationAsync(string? location)
        {
            try
            {
                var query = _context.Cars.Include(c => c.Brand).Include(c => c.Location).AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(location))
                {
                    query = query.Where(c => c.Location.Address == location);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }
        public async Task<List<CarEntity>> FilterCarsByMocelAsync(string? model)
        {
            try
            {
                var query = _context.Cars.Include(c => c.Brand).Include(c => c.Location).AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(model))
                {
                    query = query.Where(c => c.Model == model);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }
        public async Task<List<CarEntity>> SearchCarsAsync(string? searchTerm = null)
        {
            try
            {
                var query = _context.Cars.Include(c => c.Brand).Include(c => c.Location).AsQueryable();

                // Apply search term
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(c => c.Model.Contains(searchTerm) ||
                                             c.Brand.BrandName.Contains(searchTerm) ||
                                             c.Location.Address.Contains(searchTerm));
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }
        
    }
}
