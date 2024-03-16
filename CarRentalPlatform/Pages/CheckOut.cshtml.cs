﻿using System.Runtime.InteropServices.JavaScript;
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
    public string[][] BlackOutDateArray { get; set; }
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
        CartModel = SessionHelper.GetObjectFromJson<CartModel>(HttpContext.Session, "cart") == null ?
            new CartModel()
            {
                Account = new AccountDto(),
                Car = new CarDto()
                {
                    Id = 0
                }
            } : CartModel = SessionHelper.GetObjectFromJson<CartModel>(HttpContext.Session, "cart");

        if (CartModel.Car.Id != 0)
        {
            CartModel.Car = _carRepository.GetCarByIdDto(CartModel.Car?.Id).Result;
            var bookings = _bookingRepository.GetBookingsForCar(CartModel.Car.Id);
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);
            BlackOutDateArray = bookings.Select(booking => new[] { booking.StartDate.ToString("yyyy-MM-dd"), booking.EndDate.ToString("yyyy-MM-dd") }).ToArray();
        }
        else
        {
            return RedirectToPage("./index");
        }

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
        return Page();
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
            return RedirectToPage("./cars"); // Return to the page to display the error
        }
        else
        {
            CartModel = SessionHelper.GetObjectFromJson<CartModel>(HttpContext.Session, "cart") == null ?
                new CartModel()
                {
                    Account = new AccountDto(),
                    Car = new CarDto()
                    {
                        Id = 0
                    }
                } : CartModel = SessionHelper.GetObjectFromJson<CartModel>(HttpContext.Session, "cart");
        }

        //Validate Account Not Found
        var account = await _accountRepository.GetAccountById(CartModel.Account.Id);
        if (account == null)
        {
            ModelState.AddModelError(string.Empty, "Account not found.");
            return Page();
        }

        //Validate Wallet Balance
        var depositAmount = DaysBetween(StartDate, EndDate) * CartModel.Car.PricePerDay;
        if (account.WalletBalance < depositAmount)
        {
            ModelState.AddModelError(string.Empty, "Insufficient wallet balance to make this booking.");
            return Page(); // Return to the page to display the error
        }

        //Valite Overlap Booking
        var existingBookings = _bookingRepository.GetBookingsForCar(CartModel.Car.Id);
        bool isOverlapping = existingBookings.Any(booking =>
            (StartDate < booking.EndDate) && (EndDate > booking.StartDate));

        if (isOverlapping)
        {
            ModelState.AddModelError(string.Empty, "The selected date range overlaps with an existing booking.");
            return Page(); // Return to the page to display the error
        }

        // Create Entity
        BookingRequest bookingRequest = new BookingRequest()
        {
            StartDate = StartDate,
            EndDate = EndDate,
            Note = note,
            CarId = CartModel.Car.Id,
            CustomerId = AccountCheckBilling.Id,
            DepositAmount = depositAmount
        };

        //Create Booking To Database
        await _bookingRepository.CreateBooking(bookingRequest);

        // Update the wallet balance after booking is made
        account.WalletBalance -= depositAmount;
        await _accountRepository.UpdateAccount(account);
        return RedirectToPage("./Index");
    }
}