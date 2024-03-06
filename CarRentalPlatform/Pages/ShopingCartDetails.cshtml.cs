using BuildObject.Entities;
using CarRentalPlatform.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages
{
    public class ShopingCartDetailsModel : PageModel
    {
        private readonly IBookingRepository _bookingRepo;

        public ShopingCartDetailsModel(IBookingRepository bookingRepository)
        {
            _bookingRepo = bookingRepository;
        }
        public BookingEntity Booking { get; set; }

        [BindProperty]
        public bool IsLogin { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            var bookEntity = await _bookingRepo.GetBookingById(id);
            if (bookEntity == null)
            {
                return NotFound();
            }
            else
            {
                Booking = bookEntity;
            }
            IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
            return Page();
        }
    }
}
