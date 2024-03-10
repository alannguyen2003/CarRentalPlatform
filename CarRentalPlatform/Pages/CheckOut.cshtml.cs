using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages;

public class CheckOut : PageModel
{
    private readonly ICarRepository _carRepository;
    private readonly IAccountRepository _accountRepository;

    public CheckOut(ICarRepository carRepository, IAccountRepository accountRepository)
    {
        _carRepository = carRepository;
        _accountRepository = accountRepository;
    }
    
    [BindProperty]
    public bool IsLogin { get; set; }

    [BindProperty]
    public CartModel? CartModel { get; set; }

    [BindProperty] public AccountCheckBilling? AccountCheckBilling { get; set; }
    
    public void OnGet()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        CartModel = SessionHelper.GetObjectFromJson<CartModel>(HttpContext.Session, "cart") == null?
            new CartModel()
            {
                Account = new AccountDto(),
                Car = new CarDto()
                {
                    Id = 0
                }
            } : CartModel = SessionHelper.GetObjectFromJson<CartModel>(HttpContext.Session, "cart");
        if (CartModel.Car.Id != 0) CartModel.Car = _carRepository.GetCarByIdDto(CartModel.Car?.Id).Result;

    }
}