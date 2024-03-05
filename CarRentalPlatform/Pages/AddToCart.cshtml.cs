using BuildObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;

namespace CarRentalPlatform.Pages
{
    public class AddToCartModel : PageModel
    {
        private readonly CarRepository _carRepo;
        private readonly BookingRepository _bookingRepo;

        public AddToCartModel(CarRepository carRepo, BookingRepository bookingRepo)
        {
            _carRepo = carRepo;
            _bookingRepo = bookingRepo;
        }

        public CarEntity CarEntity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var cars = await _carRepo.GetAllCars();
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
            return Page();
        }

        public BookingEntity BookingEntity { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            await _bookingRepo.CreateBooking(BookingEntity);
            return Page();
        }
    }
}
