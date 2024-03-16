using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages;

public class CarCategory : PageModel
{
    private readonly ICarRepository _carRepository;

    public CarCategory(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }
    
    [BindProperty]
    public bool IsLogin { get; set; }

    public AccountDto account { get; set; }

    [BindProperty]
    public CarCategoryPage CarCategoryPage { get; set; }

    [BindProperty] public int CarId { get; set; }

    public IActionResult OnGet()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        account = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        if (IsLogin == false)
        {
            return RedirectToPage("./login");
        }
        CarCategoryPage = _carRepository.GetDataCarCategoryPage().Result;
        return null;
    }

    public IActionResult OnPostAddToCart()
    {
        int id = Int32.Parse(Request.Form["id"]);
        account = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        CartModel cartModel = new CartModel()
        {
            Account = new AccountDto()
            {
                Id = account.Id,
                Email = account.Email,
                Name = account.Name
            },
            Car = new CarDto()
            {
                Id = id
            }
        };
        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cartModel);
        return RedirectToPage("./Cart");
    }
}