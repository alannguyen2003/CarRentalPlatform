using BusinessObject.Entities;
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
        Task<bool> ModifyAccountFromRequest(AccountRequest request);
        Task<AccountEntity?> GetAccountById(int id);
        Task<List<AccountResponse>> GetAllAccountResponse();
        Task<List<AccountEntity>> GetAllAccounts();
        Task UpdateAccount(AccountEntity entity);
        Task DeleteAccount(AccountEntity entity);
        Task UpdateDriverLicenseInfo(int accountId, LicenseInfo licenseInfo);

        Task<AccountResponse?> GetAccountForRequest(int id);
        Task<AccountRequest?> GetAccountForEdit(int id);

    }
}
