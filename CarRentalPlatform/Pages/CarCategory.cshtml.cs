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
    [BindProperty]
    public CarCategoryPage CarCategoryPage { get; set; }

    [BindProperty] public int CarId { get; set; }

    public void OnGet()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        CarCategoryPage = _carRepository.GetDataCarCategoryPage().Result;
    }

    public IActionResult OnPostAddToCart()
    {
        int id = Int32.Parse(Request.Form["id"]);
        AccountDto account = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        CartModel cartModel = new CartModel()
        {
            Account = new AccountDto()
            {
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