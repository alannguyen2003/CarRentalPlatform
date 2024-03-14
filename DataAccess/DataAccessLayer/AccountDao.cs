using System.Diagnostics;
using BuildObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccessLayer;

public class AccountDAO : BaseDao<AccountEntity>
{
    private readonly ApplicationDbContext _context;
    public AccountDAO() => _context = new ApplicationDbContext();

    public async Task<AccountEntity?> Authentication(string email, string password)
    {
        var account = _context.Accounts
            .First(account => account.Email == email && account.Password == password);
        return account;
    }
}