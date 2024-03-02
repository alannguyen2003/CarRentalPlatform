using BuildObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccessLayer;

public class AccountDAO : BaseDao<AccountEntity>
{
    private static AccountDAO instance = null;
    private static readonly object instanceLock = new object();
    private AccountDAO() { }

    public static AccountDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
            }
            return instance;
        }
    }

    public async Task<IList<AccountEntity>> GetAccountAsync()
    {
        try
        {
            var _dbContext = new ApplicationDbContext();
            return await _dbContext.Accounts.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}" + ex);
        }
    }
}