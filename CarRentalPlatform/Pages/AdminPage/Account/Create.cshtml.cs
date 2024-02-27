using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;

namespace CarRentalPlatform.Pages.AdminPage.Account
{
    public class CreateModel : PageModel
    {
        private readonly AccountDao _accountDao;


        public CreateModel( AccountDao accountDao)
        {
            _accountDao = accountDao;
        }

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
                var account = await _accountDao.GetAll();
                if (account == null || AccountEntity == null)
                {
                    return Page();
                }
                AccountEntity.WalletBalance = 0;
                AccountEntity.Role = 3;
                await _accountDao.Create(AccountEntity);

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }
    }
}
