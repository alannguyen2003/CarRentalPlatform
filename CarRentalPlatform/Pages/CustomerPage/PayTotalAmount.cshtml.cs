using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.CustomerPage
{
    public class PayTotalAmountModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IAccountRepository _accountRepository;


        public PayTotalAmountModel(IBookingRepository bookingRepository, IAccountRepository accountRepository)
        {
            _bookingRepository = bookingRepository;
            _accountRepository = accountRepository;
        }
        public async Task<IActionResult> OnGet(int id)
        {

            var booking = await _bookingRepository.GetBookingById(id);
            if (booking == null || booking.TotalAmount == null)
            {
                return RedirectToPage("/BookingHistory");
            }

            var account = await _accountRepository.GetAccountById(booking.CustomerId);
            if (account.WalletBalance >= booking.TotalAmount)
            {
                // Deduct the amount from the wallet and update
                account.WalletBalance -= (booking.TotalAmount + booking.DepositAmount);
                await _accountRepository.UpdateAccount(account);

                // Optionally, update booking status to Completed or Paid
                booking.Status = 4; // Assuming 4 is the status for Completed/Paid
                await _bookingRepository.UpdateBooking(booking);

                // Redirect to Booking History with a success message
                TempData["SuccessMessage"] = "Payment successful";
            }
            else
            {
                // Redirect to Booking History with an error message
                TempData["ErrorMessage"] = "Insufficient balance in wallet";
            }
            return RedirectToPage("./BookingHistory");
        }

    }
}
