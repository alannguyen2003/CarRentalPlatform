using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.EmployeePage
{
    public class TakeCarModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;
        public TakeCarModel(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            await _bookingRepository.UpdateBookingStatus(id, 2);
            return RedirectToPage("./BookingHistory");
        }
    }
}
