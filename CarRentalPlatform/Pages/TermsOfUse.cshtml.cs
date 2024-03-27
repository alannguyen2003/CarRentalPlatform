using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRentalPlatform.Pages
{
    public class TermsOfUseModel : PageModel
    {
        [BindProperty]
        public bool IsLogin { get; set; }
        public AccountDto Account { get; set; }
        public IActionResult OnGet()
        {
            IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
            Account = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
            if (IsLogin == false)
            {
                return RedirectToPage("./login");
            }
            return null;
        }
    }
}
