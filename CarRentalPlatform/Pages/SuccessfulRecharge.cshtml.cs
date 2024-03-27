using BusinessObject.Entities;
using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages;

public class SuccessfulRecharge : PageModel
{
    private readonly IAccountRepository _accountRepository;
    
    [BindProperty]
    public bool IsLogin { get; set; }
    
    [BindProperty]
    public AccountEntity? UserAccount { get; set; }

    public SuccessfulRecharge(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<IActionResult> OnGet(int money)
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        AccountDto? accountDto = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        if (accountDto != null)
        {
            UserAccount = await _accountRepository.GetAccountById(accountDto.Id);
            if (UserAccount == null)
            {
                return Redirect("/Error");
            }
            UserAccount.WalletBalance += money;
            await _accountRepository.UpdateAccount(UserAccount);
            return Redirect("/Index");
        }
        else
        {
            return Redirect("/Login");
        }
    }
}