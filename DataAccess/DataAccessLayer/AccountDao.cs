using System.Diagnostics;
using BuildObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using DataTransferLayer.DataTransfer;
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

    public async Task<AccountEntity?> CheckExistEmail(string email)
    {
        if (_context.Accounts != null)
        {
            var account = _context.Accounts
                .First(account => account.Email == email);
            return account;
        }
        return null;
    }
    
    public async Task<AccountEntity?> CheckExistPhoneNumber(string phoneNumber)
    {
        if (_context.Accounts != null)
        {
            var account = _context.Accounts
                .First(account => account.PhoneNumber == phoneNumber);
            return account;
        }
        return null;
    }
}

