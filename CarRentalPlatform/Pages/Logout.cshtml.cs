using CarRentalPlatform.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRentalPlatform.Pages;

public class Logout : PageModel
{
    
    public IActionResult OnGet()
    {
        SessionHelper.ClearSession(HttpContext.Session);
        return RedirectToPage("./Login");
    }
}