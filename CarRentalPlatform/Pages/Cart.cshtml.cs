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
    
    [BindProperty]
    public bool IsLogin { get; set; }
    

    [BindProperty]
    public CartModel? CartModel { get; set; }

    public Cart(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

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