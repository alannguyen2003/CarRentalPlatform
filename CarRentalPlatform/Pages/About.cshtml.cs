using CarRentalPlatform.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRentalPlatform.Pages;

public class About : PageModel
{
    [BindProperty]
    public bool IsLogin { get; set; }
    public IActionResult OnGet()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        if (IsLogin == false)
        {
            return RedirectToPage("./login");
        }
        return null;
    }
}