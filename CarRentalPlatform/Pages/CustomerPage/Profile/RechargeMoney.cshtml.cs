using BusinessObject.Entities;
using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.CustomerPage.Profile;

public class RechargeMoney : PageModel
{
    private readonly IAccountRepository _accountRepository;
    
    [BindProperty]
    public bool IsLogin { get; set; }
    [BindProperty]
    public AccountEntity UserAccount { get; set; }

    [TempData]
    public string Message { get; set; }

    public RechargeMoney(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        AccountDto accountDto = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        if (accountDto != null)
        {
            UserAccount = await _accountRepository.GetAccountById(accountDto.Id);
            if (UserAccount == null)
            {
                return Redirect("/Error");
            }
        }
        else
        {
            return Redirect("/login");
        }
        return Page();
    }
}