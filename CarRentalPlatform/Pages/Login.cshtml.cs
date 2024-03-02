using CarRentalPlatform.Configuration;
using CarRentalPlatform.Configuration.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRentalPlatform.Pages;

public class Login : PageModel
{

    [BindProperty]
    public string Message { get; set; }

    [BindProperty]
    public bool Result { get; set; }
    
    public void OnGet()
    {
        Message = "";
    }

    public IActionResult OnPost()
    {
        string email = Request.Form["email"]!;
        string password = Request.Form["password"]!;
        if (email.Equals(JsonConfiguration.GetValueFromAppSettings(JsonConfiguration.AdminEmail)) &&
                         password.Equals(JsonConfiguration.GetValueFromAppSettings(JsonConfiguration.AdminPassword)))
        {
            SessionHelper.SetObjectAsJson(HttpContext.Session,"isLogin", true);
            return RedirectToPage("/Index");
        }
        else
        {
            Message = "Wrong email or password!";
        }

        return null;
    }
}