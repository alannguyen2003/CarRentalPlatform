using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;
using BuildObject.Entities;
using DataTransferLayer.DataTransfer;
using CarRentalPlatform.Configuration;
using BusinessObject.Entities;

namespace CarRentalPlatform.Pages.EmployeePage
{
    public class FixingDetailInputModel
    {
        public string FixingDescription { get; set; }
        public int Price { get; set; }
    }
        
    public class EditBookingTotalModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IFixingDetailRepository _fixingDetailRepository;

        [BindProperty]
        public BookingDetailDTO Booking { get; set; }

        [BindProperty]
        public AccountDto UserAccount { get; set; }

        [BindProperty]
        public bool IsLogin { get; set; }

        [BindProperty]
        public List<FixingDetailInputModel> FixingDetails { get; set; } = new List<FixingDetailInputModel>();

        public EditBookingTotalModel(IBookingRepository bookingRepository, IFixingDetailRepository fixingDetailRepository)
        {
            _bookingRepository = bookingRepository;
            _fixingDetailRepository = fixingDetailRepository;
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

            var bookingToUpdate = await _bookingRepository.GetBookingById(Booking.BookingId);

            if (bookingToUpdate != null)
            {
                // Handles adding each Fixing Detail to the database
                foreach (var detail in FixingDetails)
                {
                    await _fixingDetailRepository.CreateFixingDetail(new FixingDetailEntity
                    {
                        BookingId = Booking.BookingId,
                        FixingDescription = detail.FixingDescription,
                        Price = detail.Price
                    });
                }

                // Update Total Amount and Status
                bookingToUpdate.TotalAmount = FixingDetails.Sum(fd => fd.Price);
                bookingToUpdate.Status = 6; // Change status to "Paying"
                await _bookingRepository.UpdateBooking(bookingToUpdate);

                TempData["SuccessMessage"] = "Booking and fixing details updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Booking not found.";
            }

            return RedirectToPage("./EmployeeBookingManagement");
        }

    }
}
