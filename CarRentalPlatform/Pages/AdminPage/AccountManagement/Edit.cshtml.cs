using CarRentalPlatform.Configuration.Validation;
using DataTransferLayer.DataTransfer.Request;
using DataTransferLayer.DataTransfer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.AdminPage.AccountManagement;

public class Edit : PageModel
{
    private readonly IAccountRepository _accountRepository;

    public Edit(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    [BindProperty] 
    public AccountRequest? AccountRequest { get; set; }
    [BindProperty]
    public string Message { get; set; }

    public IActionResult OnGet(int id)
    {
        ResetFormData(id);
        return Page();
    }

    public IActionResult OnPost()
    {
        if (AccountRequest != null)
        {
            if (AccountValidation.IsValidEmail(AccountRequest.Email) &&
                AccountValidation.IsValidPhoneNumber(AccountRequest.PhoneNumber) &&
                AccountValidation.IsValidPassword(AccountRequest.Password))
            {
                Message = AccountRequest.Id + " " + AccountRequest.Email;
                bool checkEdit = _accountRepository.ModifyAccountFromRequest(AccountRequest).Result;
                if (checkEdit) return RedirectToPage("./index");
                else
                {
                    Message = "Error!";
                }
                return Page();

            }
            else
            {
                Message = "Your email was indentical, or your phone number has wrong format, please check again!";
            }
        }
        else
        {
            ResetFormData(Int32.Parse(Request.Form["id"]));
            Message = AccountRequest.Id + " " + AccountRequest.Email;
        }
        return Page();

    }

    public void ResetFormData(int id)
    {
        List<RoleRequest> listRoleRequest = new List<RoleRequest>();
        listRoleRequest.Add(new RoleRequest()
        {
            Id = 1,
            Role = "Admin"
        });
        listRoleRequest.Add(new RoleRequest()
        {
            Id = 2,
            Role = "Employee"
        });
        listRoleRequest.Add(new RoleRequest()
        {
            Id = 3,
            Role = "Customer"
        });
        List<GenderRequest> listGenderRequest = new List<GenderRequest>();
        listGenderRequest.Add(new GenderRequest()
        {
            Id = 0, Gender = "Male"
        });
        listGenderRequest.Add(new GenderRequest()
        {
            Id = 1, Gender = "Female"
        });
        ViewData["Role"] = new SelectList(listRoleRequest, "Id", "Role");
        ViewData["Gender"] = new SelectList(listGenderRequest, "Id", "Gender");
        AccountRequest = _accountRepository.GetAccountForEdit(id).Result;
    }
}