using System.Diagnostics;
using BuildObject.Entities;
using BusinessObject.Entities;
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

    public async Task<bool?> CheckExistEmail(string email)
    {
        if (_context.Accounts != null)
        {
            var isFound = _context.Accounts
                .Any(account => account.Email == email);
            return isFound;
        }
        return false;
    }

    public async Task<bool?> CheckExistPhoneNumber(string phoneNumber)
    {
        if (_context.Accounts != null)
        {
            var isFound = _context.Accounts
                .Any(account => account.PhoneNumber == phoneNumber);
            return isFound;
        }
        return false;
    }
}