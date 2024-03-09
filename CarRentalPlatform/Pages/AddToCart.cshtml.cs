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

        public AddToCartModel(ICarRepository carRepo, IBookingRepository bookingRepo)
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
            if (BookingEntity == null)
            {
                throw new ArgumentNullException(nameof(BookingEntity), "Entity cannot be null.");
            }
            //BookingEntity.CustomerId =
            await _bookingRepo.CreateBooking(BookingEntity);
            return Page();
        }
    }
}
