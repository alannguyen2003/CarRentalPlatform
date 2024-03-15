﻿using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferLayer.DataTransfer;
using Microsoft.EntityFrameworkCore;

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
                    Email = account.Email
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

        public async Task<List<AccountEntity>> GetAllAccounts() => await _accountDao.GetAll().Result.ToListAsync();

        public Task<AccountEntity?> GetAccountById(int id) => _accountDao.GetEntityById(id);

        public Task UpdateAccount(AccountEntity entity) => _accountDao.UpdateEntity(entity);

        public Task DeleteAccount(AccountEntity entity) => _accountDao.DeleteEntity(entity);
    }
}
