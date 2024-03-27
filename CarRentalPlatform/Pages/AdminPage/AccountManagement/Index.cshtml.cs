using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.DataTransfer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.AdminPage.AccountManagement;

public class IndexModel : PageModel
{
    private readonly IAccountRepository _accountRepository;

    public IndexModel(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    [BindProperty]
    public List<AccountResponse> ListAccounts { get; set; }
    [BindProperty]
    public bool IsLogin { get; set; }

    [BindProperty]
    public AccountDto AccountDto { get; set; }

    public IActionResult OnGet()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        AccountDto = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        if (IsLogin == false)
        {
            return RedirectToPage("/login");
        }
        else if (AccountDto.Role != 1)
        {
            return RedirectToPage("/index");
        }
        ListAccounts = _accountRepository.GetAllAccountResponse().Result;        
        return null;
    }
}