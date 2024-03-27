using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.EmployeePage
{
    public class ViewBookingDetailModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;

        [BindProperty]
        public BookingDetailDTO Booking { get; set; }

        [BindProperty]
        public AccountDto UserAccount { get; set; }

        [BindProperty]
        public bool IsLogin { get; set; }
        public ViewBookingDetailModel(IBookingRepository bookingRepository)
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
    }
}
