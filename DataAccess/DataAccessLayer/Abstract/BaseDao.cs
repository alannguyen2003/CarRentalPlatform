using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccessLayer.Abstract;

public class BaseDao<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public BaseDao()
    {
        _context = new ApplicationDbContext();
    }

    public async Task Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IQueryable<T>> GetAll()
    {
        try
        {
            return _context.Set<T>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}" + ex);
        }
    }
    
    public async Task<T?> GetEntityById(int id)
    {
        try
        {
            return await _context.Set<T>().FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}" + ex);
        }
    }
    
    public async Task UpdateEntity(T entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}" + ex);
        }
    }
    
    public async Task DeleteEntity(T entity)
    {
        try
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}" + ex);
        }
    }
}