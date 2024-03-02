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
    public class DeleteModel : PageModel
    {
        private readonly IAccountRepository _accountRepository = new AccountRepository();

     [BindProperty]
    public AccountEntity AccountEntity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                var accounts = await _accountRepository.GetAllAccounts();
                if (id == null || accounts == null)
                {
                    return NotFound();
                }

                var accountentity = await _accountRepository.GetAccountById((int)id);

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
                var getaccounts = _accountRepository.GetAllAccounts();
                if (id == null || getaccounts == null)
                {
                    return NotFound();
                }
                var accountentity = await _accountRepository.GetAccountById((int)id);

                if (accountentity != null)
                {
                    AccountEntity = accountentity;
                    await _accountRepository.DeleteAccount(AccountEntity);
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
