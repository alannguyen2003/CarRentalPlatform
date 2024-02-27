using CarRentalPlatform.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRentalPlatform.Pages;

public class Pricing : PageModel
{
    [BindProperty]
    public bool IsLogin { get; set; }
    public void OnGet()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
    }
}