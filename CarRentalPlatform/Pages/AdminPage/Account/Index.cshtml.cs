using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;

namespace CarRentalPlatform.Pages.AdminPage.Account
{
    public class IndexModel : PageModel
    {
        private readonly AccountDao _accountDao;

        public IndexModel(AccountDao accountDao)
        {
            _accountDao = accountDao;
        }

        public IList<AccountEntity> AccountEntity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                var accountsQuery = await _accountDao.GetAll();

                if (accountsQuery != null)
                {
                    AccountEntity = await accountsQuery.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }
    }
}
