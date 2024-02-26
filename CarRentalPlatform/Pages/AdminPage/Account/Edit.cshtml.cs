using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;

namespace CarRentalPlatform.Pages.AdminPage.Account
{
    public class EditModel : PageModel
    {
        private readonly AccountDao _accountDao;
        public EditModel(AccountDao accountDao)
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
                await _accountDao.UpdateEntity(AccountEntity);
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
          return (await _accountDao.GetEntityById(id)) != null;
        }
    }
}
