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
        private readonly IAccountRepository _accountRepository;


        public List<BookingDetailDTO> Bookings { get; set; }

		[BindProperty]
		public AccountDto UserAccount { get; set; }

		[BindProperty]
		public bool IsLogin { get; set; }
		public BookingHistoryModel(IBookingRepository bookingRepository, IAccountRepository accountRepository)
		{
			_bookingRepository = bookingRepository;
            _accountRepository = accountRepository;
		}

		public async Task<IActionResult> OnGet()
		{
			IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
			if (!IsLogin)
			{
				return RedirectToPage("./login");
			}

			UserAccount = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
			if (UserAccount != null)
			{
                var currentDate = DateTime.Now.Date;
                Bookings = await _bookingRepository.GetBookingDetailsByCustomerID(UserAccount.Id);

                var bookingsToUpdate = Bookings.Where(b => b.StartDate < currentDate && b.Status < 2).ToList();
                foreach (var booking in bookingsToUpdate)
                {
                    await _bookingRepository.UpdateBookingStatus(booking.BookingId, 5); // 5 is status for "Cancel"
                }

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
            UserAccount = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
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

            // Update Account Balance
            var accountToUpdate = await _accountRepository.GetAccountById(UserAccount.Id);
            accountToUpdate.WalletBalance += booking.DepositAmount;
            await _accountRepository.UpdateAccount(accountToUpdate);
            return RedirectToPage("./BookingHistory");
        }

    }
}
