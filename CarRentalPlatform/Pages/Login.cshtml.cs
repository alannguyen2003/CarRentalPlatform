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
    
    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        
        if (!ModelState.IsValid) return Page();
        try
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
                    Name = JsonConfiguration.GetValueFromAppSettings(JsonConfiguration.AdminPassword),
                    Role = 1
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "isLogin", true);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "user", (object)account);
                return RedirectToPage("./adminpage/account/index");
            }
            else if (accountCheck != null)
            {
                AccountDto? accountDto = _accountRepository.Login(email, password).Result;
                AccountDto? account = new AccountDto()
                {
                    Id = accountDto.Id,
                    Email = accountDto.Email,
                    Name = accountDto.Name,
                    WalletBalance = accountDto.WalletBalance,
                    Role = accountDto.Role
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "isLogin", true);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "user", (object)account);
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError("Account", "Invalid account");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("Account", "An error occured during login proccess");
        }
        return Page();
    }
}