using BuildObject.Entities;
using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.CustomerPage
{
    public class BookingHistoryModel : PageModel
    {
		private readonly IBookingRepository _bookingRepository;

		public List<BookingDetailDTO> Bookings { get; set; }

		[BindProperty]
		public AccountDto UserAccount { get; set; }

		[BindProperty]
		public bool IsLogin { get; set; }
		public BookingHistoryModel(IBookingRepository bookingRepository)
		{
			_bookingRepository = bookingRepository;
		}

		public async Task<IActionResult> OnGet()
		{
			IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
			if (!IsLogin)
			{
				return RedirectToPage("./login");
			}

			UserAccount = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
			if (UserAccount != null && UserAccount.Role == 3)
			{
				Bookings = await _bookingRepository.GetBookingDetailsByCustomerID(UserAccount.Id);
			}
			else
			{
				return RedirectToPage("/Error");
			}
			return Page();
		}

        public async Task<IActionResult> OnGetCancelAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }

            var now = DateTime.UtcNow;
            var timeDiff = booking.StartDate - now;

            if (timeDiff.TotalHours < 24)
            {
				// Set error message to display it somehow or log it
				ModelState.AddModelError(string.Empty, "The automatic cancellation function only allows orders that are within 24 hours of the start date. Please contact staff for further assistance.");
                // This example simply redirects back with an error query string
                return RedirectToPage("./BookingHistory");
            }

            await _bookingRepository.UpdateBookingStatus(id, 5); // 5 is status for "Cancel"
            return RedirectToPage("./BookingHistory");
        }

    }
}
