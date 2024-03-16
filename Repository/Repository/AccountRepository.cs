using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.DataTransfer.Request;
using DataTransferLayer.DataTransfer.Response;
using Microsoft.EntityFrameworkCore;
using Repository.Repository.Utils;

namespace Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _accountDao;

        public AccountRepository()
        {
            _accountDao = new AccountDAO();
        }

        public async Task<AccountDto?> Login(string email, string password)
        {
            var account = await _accountDao.Authentication(email, password);
            if (account != null)
            {
                return new AccountDto()
                {
                    Id = account.Id,
                    Name = account.LastName,
                    Email = account.Email,
                    Role = account.Role
                };
            }
            return null;
        }

        public async Task<AccountCheckBilling?> GetAccountCheckBilling(int id)
        {
            var account = await _accountDao.GetEntityById(id);
            return new AccountCheckBilling()
            {
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                DriverLicense = account.DriverLicense,
                PhoneNumber = account.PhoneNumber
            };
        }

        public async Task UpdateDriverLicenseInfo(int accountId, LicenseInfo licenseInfo)
        {
            var account = await _accountDao.GetEntityById(accountId);
            if (account != null)
            {
                // Map infor from LicenseInfo to AccountEntity
                account.DriverLicense = licenseInfo.DriverLicense;

                // UpdateEntity
                await _accountDao.UpdateEntity(account);
            }
            else
            {
                throw new Exception("Account not found");
            }
        }
        
        

        public Task CreateAccount(AccountEntity entity) => _accountDao.Create(entity);
        public async Task<bool> CreateAccountFromRequest(CreateAccountRequest request)
        {
            var account = _accountDao.CheckExistEmail(request.Email).Result;
            var account1 = _accountDao.CheckExistPhoneNumber(request.PhoneNumber).Result;
            if (account != null && account1 != null)
            {
                AccountEntity entity = new AccountEntity()
                {
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = request.Password,
                    Role = request.Role,
                    WalletBalance = request.WalletBalance,
                    Gender = ConvertUtilization.GetGender_2(request.Gender),
                };
                await _accountDao.Create(entity);
                return true;
            }
            return false;
        }

        public async Task<List<AccountResponse>> GetAllAccountResponse()
        {
            var accounts = await _accountDao.GetAll().Result.ToListAsync();
            List<AccountResponse> listResponse = new List<AccountResponse>();
            foreach (var item in accounts)
            {
                AccountResponse accountResponse = new AccountResponse()
                {
                    Id = item.Id,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    DriverLicense = item.DriverLicense,
                    FirstName = item.FirstName,
                    LastName = item.LastName, 
                    Role = ConvertUtilization.GetRole(item.Role),
                    Gender = ConvertUtilization.GetGender(item.Gender),
                    WalletBalance = item.WalletBalance
                };
                listResponse.Add(accountResponse);
            }
            return listResponse;
        }

        public async Task<List<AccountEntity>> GetAllAccounts() => await _accountDao.GetAll().Result.ToListAsync();

        public Task<AccountEntity?> GetAccountById(int id) => _accountDao.GetEntityById(id);

        public Task UpdateAccount(AccountEntity entity) => _accountDao.UpdateEntity(entity);

        public Task DeleteAccount(AccountEntity entity) => _accountDao.DeleteEntity(entity);
    }
}
