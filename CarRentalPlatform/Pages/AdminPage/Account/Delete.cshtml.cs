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
    public class DeleteModel : PageModel
    {
        private readonly AccountDao _accountDao;


        public DeleteModel( AccountDao accountDao)
        {
            _accountDao = accountDao;
        }

        [BindProperty]
      public AccountEntity AccountEntity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                var accounts = await _accountDao.GetAll();
                if (id == null || accounts == null)
                {
                    return NotFound();
                }

                var accountentity = await _accountDao.GetEntityById((int)id);

                if (accountentity == null)
                {
                    return NotFound();
                }
                else
                {
                    AccountEntity = accountentity;
                }
                return Page();
            }
            catch (Exception ex) { throw new Exception($"Error: {ex.Message}", ex); }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                var getaccounts = _accountDao.GetAll();
                if (id == null || getaccounts == null)
                {
                    return NotFound();
                }
                var accountentity = await _accountDao.GetEntityById((int)id);

                if (accountentity != null)
                {
                    AccountEntity = accountentity;
                    await _accountDao.DeleteEntity(AccountEntity);
                }

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }
    }
}
