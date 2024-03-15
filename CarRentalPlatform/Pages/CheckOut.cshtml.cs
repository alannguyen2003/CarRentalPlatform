using System.Runtime.InteropServices.JavaScript;
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
    private readonly IBookingRepository _bookingRepository;

    public CheckOut(ICarRepository carRepository, IAccountRepository accountRepository, IBookingRepository bookingRepository)
    {
        _carRepository = carRepository;
        _accountRepository = accountRepository;
        _bookingRepository = bookingRepository;
    }
    
    [BindProperty]
    public bool IsLogin { get; set; }

    [BindProperty]
    public CartModel? CartModel { get; set; }

    [BindProperty] 
    public AccountCheckBilling? AccountCheckBilling { get; set; }
    [BindProperty]
    public DateTime StartDate { get; set; }
    [BindProperty]
    public DateTime EndDate { get; set; }
    
    public IActionResult OnGet()
    {
        IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
        if (IsLogin == false)
        {
            return RedirectToPage("./login");
        }
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
        var account = _accountRepository.GetAccountById(CartModel.Account.Id).Result;
        if (account != null)
        {
            AccountCheckBilling = new AccountCheckBilling()
            {
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                PhoneNumber = account.PhoneNumber,
                DriverLicense = account.DriverLicense
            };
        }
        return null;
    }
    
    public static int DaysBetween(DateTime startDate, DateTime endDate)
    {
        return (int)(endDate - startDate).TotalDays;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string note = Request.Form["note"];
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
        BookingRequest bookingRequest = new BookingRequest()
        {
            StartDate = StartDate,
            EndDate = EndDate,
            Note = note,
            CarId = CartModel.Car.Id,
            CustomerId = AccountCheckBilling.Id,
            DepositAmount = DaysBetween(StartDate, EndDate) * CartModel.Car.PricePerDay
        };
        await _bookingRepository.CreateBooking(bookingRequest);
        return RedirectToPage("./Index");
    }
}