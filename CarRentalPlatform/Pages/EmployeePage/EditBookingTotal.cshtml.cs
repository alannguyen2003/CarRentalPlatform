using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;
using BuildObject.Entities;
using DataTransferLayer.DataTransfer;
using CarRentalPlatform.Configuration;

namespace CarRentalPlatform.Pages.EmployeePage
{
    public class EditBookingTotalModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;

        [BindProperty]
        public BookingDetailDTO Booking { get; set; }

        [BindProperty]
        public AccountDto UserAccount { get; set; }

        [BindProperty]
        public bool IsLogin { get; set; }
        public EditBookingTotalModel(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");

            if (!IsLogin)
            {
                return RedirectToPage("./login");
            }

            UserAccount = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
            Booking = await _bookingRepository.GetBookingDetailsById(id);
            if (Booking == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var bookingToUpdate = await _bookingRepository.GetBookingById(Booking.BookingId);

            if (bookingToUpdate == null)
            {
                return NotFound();
            }

            bookingToUpdate.TotalAmount = Booking.TotalAmount;
            await _bookingRepository.UpdateBooking(bookingToUpdate);

            return RedirectToPage("./EmployeeBookingManagement");
        }
    }
}
