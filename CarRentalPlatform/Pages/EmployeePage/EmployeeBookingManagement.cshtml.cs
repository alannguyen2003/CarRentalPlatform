using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;
using BuildObject.Entities;
using DataTransferLayer.DataTransfer;
using CarRentalPlatform.Configuration;

namespace CarRentalPlatform.Pages.EmployeePage
{
    public class EmployeeBookingManagementModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;

        public List<BookingDetailDTO> Bookings { get; set; } = new List<BookingDetailDTO>();

        [BindProperty]
        public AccountDto UserAccount { get; set; }

        [BindProperty]
        public bool IsLogin { get; set; }

        public EmployeeBookingManagementModel(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");

            if (!IsLogin)
            {
                return RedirectToPage("./login");
            }

            UserAccount = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
            if (UserAccount == null || UserAccount.Role != 2) // Assuming Role 2 is for Employee
            {
                return RedirectToPage("/Error");
            }

            var currentDate = DateTime.Now.Date;
            Bookings = await _bookingRepository.GetAllBookingDetails();

            var bookingsToUpdate = Bookings.Where(b => b.StartDate < currentDate && b.Status < 2).ToList();
            foreach (var booking in bookingsToUpdate)
            {
                await _bookingRepository.UpdateBookingStatus(booking.BookingId, 5); // 5 is status for "Cancel"
            }

            // Reload bookings after updates
            Bookings = await _bookingRepository.GetAllBookingDetails();

            return Page();
        }


        public async Task<IActionResult> OnGetCancelAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }

            await _bookingRepository.UpdateBookingStatus(id, 5); // 5 is status for "Cancel"
            return RedirectToPage("./EmployeeBookingManagement");
        }
    }
}

