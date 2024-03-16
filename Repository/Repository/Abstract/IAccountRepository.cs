using BuildObject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.DataTransfer.Request;
using DataTransferLayer.DataTransfer.Response;

namespace Repository.Repository.Abstract
{
    public interface IAccountRepository
    {
        Task<AccountDto?> Login(string email, string password);
        Task<AccountCheckBilling?> GetAccountCheckBilling(int id);
        Task CreateAccount(AccountEntity entity);
        Task<bool> CreateAccountFromRequest(CreateAccountRequest request);
        Task<AccountEntity?> GetAccountById(int id);
        Task<List<AccountResponse>> GetAllAccountResponse();
        Task<List<AccountEntity>> GetAllAccounts();
        Task UpdateAccount(AccountEntity entity);
        Task DeleteAccount(AccountEntity entity);
        Task UpdateDriverLicenseInfo(int accountId, LicenseInfo licenseInfo);

    }
}
