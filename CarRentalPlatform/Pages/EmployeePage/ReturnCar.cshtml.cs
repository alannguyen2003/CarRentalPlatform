using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.EmployeePage
{
    public class ReturnCarModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;

        public ReturnCarModel(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await _bookingRepository.UpdateBookingStatus(id, 3);

            // Update Actual Return Date to today's date
            await _bookingRepository.UpdateActualReturnDate(id, DateTime.Now);
            return RedirectToPage("./EmployeeBookingManagement");
        }
    }

}
