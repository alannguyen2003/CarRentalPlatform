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
    public class DetailsModel : PageModel
    {
        private readonly AccountDAO _accountDao;
        public DetailsModel( AccountDAO accountDao)
        {
            _accountDao = accountDao;
        }

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
                    var genderValue = accountentity.Gender;
                    ViewData["DisplayGender"] = genderValue == true ? "Male" : "Female";
                }
                return Page();
            }
            catch (Exception ex) { throw new Exception($"Error: {ex.Message}", ex); }
        }
    }
}
