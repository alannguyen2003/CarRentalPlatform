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

		public List<BookingEntity> Bookings { get; set; }

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
				Bookings = await _bookingRepository.GetBookingsByCustomerId(UserAccount.Id);
			}
			else
			{
				return RedirectToPage("/Error");
			}
			return Page();
		}
	}
}
