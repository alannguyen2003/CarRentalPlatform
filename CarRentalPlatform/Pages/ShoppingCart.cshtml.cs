using BuildObject.Entities;
using CarRentalPlatform.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository;
using Repository.Repository.Abstract;
using System.Text.Json;

namespace CarRentalPlatform.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;

        public ShoppingCartModel(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

       public IList<BookingEntity> BookingEntities { get; set; }
        public bool IsLogin { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var booking = await _bookingRepository.GetAllBookings();
            IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
            return Page();
        }
    }
}
