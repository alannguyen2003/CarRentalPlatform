using BuildObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages
{
    public class AddToCartModel : PageModel
    {
        private readonly ICarRepository _carRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IAccountRepository _accountRepo;

        public AddToCartModel(ICarRepository carRepo, IBookingRepository bookingRepo, IAccountRepository accountRepo)
        {
            _carRepo = carRepo;
            _bookingRepo = bookingRepo;
            _accountRepo = accountRepo;
        }

        public CarEntity CarEntity { get; set; } = default!;

        public AccountEntity Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var cars = await _carRepo.GetAllCars();
            var cus = await _accountRepo.GetAccountById(1);
            if (id == null || cars == null)
            {
                return NotFound();
            }

            var carentity = await _carRepo.GetCarById(id);
            if (carentity == null)
            {
                return NotFound();
            }
            else
            {
                CarEntity = carentity;                
            }

            TempData["CarId"] = id;
            return Page();
        }

        public BookingEntity BookingEntity { get; set; }
        public async Task<IActionResult> OnPostAsync(BookingEntity bookingEntity)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!TempData.TryGetValue("CarId", out var carIdObj) || !(carIdObj is int id))
            {
                return NotFound();
            }
            var carEntity = await _carRepo.GetCarById(id);

            if (carEntity == null)
            {
                return NotFound();
            }

            // Update bookingEntity with form data and carEntity
            bookingEntity.Car = carEntity;

            bookingEntity.StartDate = BookingEntity.StartDate;
            bookingEntity.EndDate = BookingEntity.EndDate;

            await _bookingRepo.CreateBooking(bookingEntity);

            return RedirectToPage("/ShoppingCart");
        }
    }
}
