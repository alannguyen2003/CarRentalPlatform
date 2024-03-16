using CarRentalPlatform.Configuration.Validation;
using DataTransferLayer.DataTransfer.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.AdminPage.AccountManagement;

public class Create : PageModel
{
    private readonly IAccountRepository _accountRepository;

    public Create(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    [BindProperty]
    public CreateAccountRequest CreateAccountRequest { get; set; }
    [BindProperty]
    public string Message { get; set; }
    
    public IActionResult OnGet()
    {
        ResetFormData();
        return Page();
    }

    public IActionResult OnPost()
    {
        int gender = Int32.Parse(Request.Form["gender"]);
        if (AccountValidation.IsValidEmail(CreateAccountRequest.Email) &&
            AccountValidation.IsValidPhoneNumber(CreateAccountRequest.PhoneNumber) &&
            AccountValidation.IsValidPassword(CreateAccountRequest.Password))
        {
            
        } 
        ResetFormData();
        return Page();
    }

    public void ResetFormData()
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
        ViewData["RoleId"] = new SelectList(listRoleRequest, "Id", "Role");
        ViewData["GenderId"] = new SelectList(listGenderRequest, "Id", "Gender");
    }
}