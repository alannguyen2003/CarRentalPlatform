using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using Repository.Repository;

namespace CarRentalPlatform.Pages.AdminPage.Account
{
    public class CreateModel : PageModel
    {
        private readonly IAccountRepository _accountRepository = new AccountRepository();

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountEntity AccountEntity { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var accounts = await _accountRepository.GetAllAccounts();
                if (accounts == null || AccountEntity == null)
                {
                    return Page();
                }
                AccountEntity.WalletBalance = 0;
                AccountEntity.Role = 3;
                await _accountRepository.CreateAccount(AccountEntity);

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }
    }
}
