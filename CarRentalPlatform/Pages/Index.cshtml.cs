using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICarRepository _carRepository;
    
    [BindProperty]
    public bool IsLogin { get; set; }
    public AccountDto Account { get; set; }

    [BindProperty]
    public CarCategoryPage CarIndexPage { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public IActionResult OnGet()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        Account = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        if (IsLogin == false)
        {
            return RedirectToPage("./login");
        }
        CarIndexPage = _carRepository.GetDataCarCategoryPage().Result;
        return null;
    }
}