using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.DataTransfer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Packaging.Core;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.AdminPage.CarManagement;

public class Detail : PageModel
{
    private readonly ICarRepository _carRepository;

    public Detail(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }
    
    [BindProperty]
    public CarResponse? CarResponse { get; set; }
    [BindProperty]
    public bool IsLogin { get; set; }

    [BindProperty]
    public AccountDto AccountDto { get; set; }

    public IActionResult OnGet(int id)
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        AccountDto = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        if (IsLogin == false)
        {
            return RedirectToPage("/login");
        }
        else if (AccountDto.Role != 1)
        {
            return RedirectToPage("/index");
        }
        return Page();
    }

    public void ResetFormData(int id)
    {
        CarResponse = null;
    }
}