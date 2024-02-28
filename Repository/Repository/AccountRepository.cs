using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public Task CreateAccount(AccountEntity entity) => AccountDAO.Instance.Create(entity);

        public Task<IList<AccountEntity>> GetAllAccounts() => AccountDAO.Instance.GetAccountAsync();

        public Task<AccountEntity?> GetAccountById(int id) => AccountDAO.Instance.GetEntityById(id);

        public Task UpdateAccount(AccountEntity entity) => AccountDAO.Instance.UpdateEntity(entity);

        public Task DeleteAccount(AccountEntity entity) => AccountDAO.Instance.DeleteEntity(entity);
    }
}
