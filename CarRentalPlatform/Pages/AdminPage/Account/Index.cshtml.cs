using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using CarRentalPlatform.Configuration;
using DataAccess.DataAccessLayer;
using DataTransferLayer.DataTransfer;
using Repository.Repository.Abstract;
using Repository.Repository;

namespace CarRentalPlatform.Pages.AdminPage.Account
{
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _accountRepository = new AccountRepository();

        public IList<AccountEntity> AccountEntity { get;set; }
        [BindProperty]
        public bool IsLogin { get; set; }
        [BindProperty]
        public AccountDto Account { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
                Account = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
                if (Account.Id != 0)
                {
                    SessionHelper.ClearSession(HttpContext.Session);
                    return RedirectToPage("/login");
                }
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
            return null;
        }
    }
}
