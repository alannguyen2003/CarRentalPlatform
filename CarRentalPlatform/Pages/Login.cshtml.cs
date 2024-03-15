using CarRentalPlatform.Configuration;
using CarRentalPlatform.Configuration.Enum;
using DataTransferLayer.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages;

public class Login : PageModel
{
    private readonly IAccountRepository _accountRepository;

    public Login(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [BindProperty]
    public string Message { get; set; }
    
    public void OnGet()
    {
        Message = "";
    }

    public IActionResult OnPost()
    {
        string email = Request.Form["email"]!;
        string password = Request.Form["password"]!;
        AccountDto? accountCheck = _accountRepository.Login(email, password).Result;
        if (email.Equals(JsonConfiguration.GetValueFromAppSettings(JsonConfiguration.AdminEmail)) &&
                         password.Equals(JsonConfiguration.GetValueFromAppSettings(JsonConfiguration.AdminPassword)))
        {
            AccountDto account = new AccountDto()
            {
                Id = 0,
                Email = JsonConfiguration.GetValueFromAppSettings(JsonConfiguration.AdminEmail),
                Name = JsonConfiguration.GetValueFromAppSettings(JsonConfiguration.AdminPassword)
            };
            SessionHelper.SetObjectAsJson(HttpContext.Session,"isLogin", true);
            SessionHelper.SetObjectAsJson(HttpContext.Session,"user", (object) account);
            return RedirectToPage("./adminpage/account/index");
        }
        else if (accountCheck != null)
        {
            AccountDto? accountDto = _accountRepository.Login(email, password).Result;
            AccountDto? account = new AccountDto()
            {
                Id = accountDto.Id,
                Email = accountDto.Email,
                Name = accountDto.Name
            };
            SessionHelper.SetObjectAsJson(HttpContext.Session,"isLogin", true);
            SessionHelper.SetObjectAsJson(HttpContext.Session,"user", (object) account);
            return RedirectToPage("./Index");
        }
        else
        {
            Message = "Wrong email or password!";
        }
        return null;
    }
}