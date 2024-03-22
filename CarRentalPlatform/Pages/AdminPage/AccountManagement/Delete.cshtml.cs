using BuildObject.Entities;
using DataTransferLayer.DataTransfer.Request;
using DataTransferLayer.DataTransfer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.AdminPage.AccountManagement;

public class Delete : PageModel
{
    private readonly IAccountRepository _accountRepository;

    public Delete(IAccountRepository accountRepository)
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
        _accountRepository.DeleteAccount(new AccountEntity()
        {
            Id = AccountResponse.Id
        });
        return RedirectToPage("./index");
    }

    public void ResetFormData(int id)
    {
        AccountResponse = _accountRepository.GetAccountForRequest(id).Result;
    }
}