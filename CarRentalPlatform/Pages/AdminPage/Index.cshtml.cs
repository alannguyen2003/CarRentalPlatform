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
    
    public void OnGet()
    {
        
    }
}