using BusinessObject.Entities;
using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repository;
using Repository.Repository.Abstract;

namespace CarRentalPlatform.Pages.EmployeePage
{
    public class ViewFixingDetailsModel : PageModel
    {
        private readonly IFixingDetailRepository _fixingDetailRepository;
        public int BookingId { get; set; }

        [BindProperty]
        public AccountDto UserAccount { get; set; }

        [BindProperty]
        public bool IsLogin { get; set; }

        [BindProperty]
        public List<FixingDetailEntity> FixingDetails { get; set; }

        public ViewFixingDetailsModel(IFixingDetailRepository fixingDetailRepository)
        {
            _fixingDetailRepository = fixingDetailRepository;
        }

        public async Task<IActionResult> OnGetAsync(int bookingId)
        {
            IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
            if (!IsLogin)
            {
                return RedirectToPage("./login");
            }

            UserAccount = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
            if (UserAccount != null)
            {
                BookingId = bookingId;
                FixingDetails = await _fixingDetailRepository.GetFixingDetailsByBookingId(BookingId);
                if (FixingDetails == null)
                {
                    return RedirectToPage("/Error");
                }
            }
            else
            {
                return RedirectToPage("/Error");
            }
            return Page();

            
        }
    }
}
