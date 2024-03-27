using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.DataTransfer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.AdminPage.CarManagement;

public class Index : PageModel
{
    private readonly ICarRepository _carRepository;

    public Index(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    [BindProperty] 
    public List<CarResponse> ListCars { get; set; }
    [BindProperty]
    public bool IsLogin { get; set; }

    [BindProperty]
    public AccountDto AccountDto { get; set; }

    public IActionResult OnGet()
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
        ListCars = _carRepository.GetAllCarResponses().Result;

        return Page();
    }
}