using BuildObject.Entities;
using BusinessObject.Entities;
using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages;

public class Cart : PageModel
{
    private readonly ICarRepository _carRepository;
    private readonly IAccountRepository _accountRepository;
    
    [BindProperty]
    public bool IsLogin { get; set; }
    

    [BindProperty]
    public CartModel? CartModel { get; set; }
    
    public Cart(ICarRepository carRepository, IAccountRepository accountRepository)
    {
        _carRepository = carRepository;
        _accountRepository = accountRepository;
    }

    public IActionResult OnGet()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        if (IsLogin == false)
        {
            return RedirectToPage("./login");
        }
        CartModel = SessionHelper.GetObjectFromJson<CartModel>(HttpContext.Session, "cart");
        if (CartModel == null)
        {
            return RedirectToPage("./cars");
        }
        else
        {
            CartModel = SessionHelper.GetObjectFromJson<CartModel>(HttpContext.Session, "cart") == null?
                new CartModel()
                {
                    Account = new AccountDto(),
                    Car = new CarDto()
                    {
                        Id = 0
                    }
                } : CartModel = SessionHelper.GetObjectFromJson<CartModel>(HttpContext.Session, "cart");
        }
        if (CartModel.Car.Id != 0) CartModel.Car = _carRepository.GetCarByIdDto(CartModel.Car?.Id).Result;
        AccountEntity? account = _accountRepository.GetAccountById(CartModel.Account.Id).Result;
        if (account != null)
        {
            CartModel.Account.Email = account.Email;
        }
        return null;
    }
}