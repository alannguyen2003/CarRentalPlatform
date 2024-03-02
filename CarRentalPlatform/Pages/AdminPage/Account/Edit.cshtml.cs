using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using CarRentalPlatform.Configuration;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using Repository.Repository;

namespace CarRentalPlatform.Pages.AdminPage.Account
{
    public class EditModel : PageModel
    {
        private readonly IAccountRepository _accountRepository = new AccountRepository();

        [BindProperty]
        public AccountEntity AccountEntity { get; set; }
        [BindProperty]
        public bool IsLogin { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
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
                AccountEntity = accountentity;
                return Page();
            }
            catch (Exception ex) { throw new Exception($"Error: {ex.Message}", ex); }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
/*            if (!ModelState.IsValid)
            {
                return Page();
            }*/
            
            try
            {
                var Account = await _accountRepository.GetAccountById(AccountEntity.Id);
                if (Account == null)
                {
                    throw new Exception();
                }
                AccountEntity = Account;
                await _accountRepository.UpdateAccount(AccountEntity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await AccountEntityExists(AccountEntity.Id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> AccountEntityExists(int id)
        {
          return (await _accountRepository.GetAccountById(id)) != null;
        }
        
}
}
