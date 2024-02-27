using BuildObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccessLayer;

public class AccountDao : BaseDao<AccountEntity>
{
    private readonly ApplicationDbContext _context;
    public AccountDao()
    {
        _context = new ApplicationDbContext();
    }
    public async Task<IList<AccountEntity>> GetAccountAsync()
    {
        try
        {
            return await _context.Accounts.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}" + ex);
        }
    }
}