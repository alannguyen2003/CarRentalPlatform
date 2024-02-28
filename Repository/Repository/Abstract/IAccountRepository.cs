using BuildObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Abstract
{
    public interface IAccountRepository
    {
        Task CreateAccount(AccountEntity entity);
        Task<AccountEntity?> GetAccountById(int id);
        Task<IList<AccountEntity>> GetAllAccounts();
        Task UpdateAccount(AccountEntity entity);
        Task DeleteAccount(AccountEntity entity);
    }
}
