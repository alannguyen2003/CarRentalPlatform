using CarRentalPlatform.Configuration;
using DataAccess.DataAccessLayer;
using DataTransferLayer.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRentalPlatform.Pages.AdminPage;

public class Index : PageModel
{
    [BindProperty]
    public bool IsLogin { get; set; }
    
    [BindProperty]
    public AccountDto AccountDto { get; set; }
    
    public IActionResult OnGet()
    {
        /*IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        AccountDto = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        if (IsLogin == false)
        {
            return RedirectToPage("/login");
        }else if (AccountDto.Role != 1)
        {
            return RedirectToPage("/index");
        }*/
        return null;

    }
}