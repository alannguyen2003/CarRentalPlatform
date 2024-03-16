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
    
    public IActionResult OnGet()
    {
        ListAccounts = _accountRepository.GetAllAccountResponse().Result;        
        return null;
    }
}