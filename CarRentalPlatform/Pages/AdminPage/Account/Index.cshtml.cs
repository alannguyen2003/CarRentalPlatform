using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using Repository.Repository;

namespace CarRentalPlatform.Pages.AdminPage.Account
{
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _accountRepository = new AccountRepository();

        public IList<AccountEntity> AccountEntity { get;set; }

        public async Task OnGetAsync()
        {
            try
            {
                var accountsQuery = await _accountRepository.GetAllAccounts();

                if (accountsQuery != null)
                {
                    AccountEntity = accountsQuery;
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }
    }
}
