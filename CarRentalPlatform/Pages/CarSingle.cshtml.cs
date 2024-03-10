using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages;

public class CarSingle : PageModel
{
    private readonly ICarRepository _carRepository;

    public CarSingle(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }
    [BindProperty]
    public AccountDto? AccountDto { get; set; }
    [BindProperty]
    public bool IsLogin { get; set; }
    [BindProperty]
    public CarDto? CarDto { get; set; }

    [BindProperty] public CartModel? CartModel { get; set; }
    
    public void OnGet(int id)
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        CarDto = _carRepository.GetCarByIdDto(id).Result;
        AccountDto = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
        CartModel = SessionHelper.GetObjectFromJson<CartModel>(HttpContext.Session, "cart") == null?
                new CartModel()
                {
                    Account = new AccountDto()
                    {
                        Id = AccountDto.Id,
                        Email = AccountDto.Email,
                        Name = AccountDto.Name
                    },
                    Car = new CarDto()
                } : CartModel = SessionHelper.GetObjectFromJson<CartModel>(HttpContext.Session, "cart");
    }
}