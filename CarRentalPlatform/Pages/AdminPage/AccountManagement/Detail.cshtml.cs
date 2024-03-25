using DataTransferLayer.DataTransfer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.AdminPage.AccountManagement;

public class Detail : PageModel
{
    private readonly IAccountRepository _accountRepository;

    public Detail(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    [BindProperty] 
    public AccountResponse? AccountResponse { get; set; }
    [BindProperty]
    public string Message { get; set; }

    public IActionResult OnGet(int id)
    {
        ResetFormData(id);
        return Page();
    }

    public IActionResult OnPost()
    {
        return Redirect("./edit?id=" + AccountResponse.Id);
    }

    public void ResetFormData(int id)
    {
        AccountResponse = _accountRepository.GetAccountForRequest(id).Result;
    }
}