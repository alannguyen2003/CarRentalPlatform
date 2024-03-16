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
            if (UserAccount != null && UserAccount.Role == 2)
            {
                Bookings = await _bookingRepository.GetAllBookingDetails();
            }
            else
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

    }
}

