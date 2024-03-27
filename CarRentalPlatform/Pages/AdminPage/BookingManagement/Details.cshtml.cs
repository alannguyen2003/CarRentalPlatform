using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.DataTransfer.Response;
using DataTransferLayer.DataTransfer.Request;
using CarRentalPlatform.Configuration;

namespace CarRentalPlatform.Pages.AdminPage.BookingManagement
{
    public class DetailsModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;
        public DetailsModel(IBookingRepository repo) { 
        _bookingRepository = repo;
        }

        [BindProperty]
        public BookingRequestAdmin BookingAdmin { get; set; } = default!;
        [BindProperty]
        public bool IsLogin { get; set; }

        [BindProperty]
        public AccountDto AccountDto { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
            AccountDto = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
            if (IsLogin == false)
            {
                return RedirectToPage("/login");
            }
            else if (AccountDto.Role != 1)
            {
                return RedirectToPage("/index");
            }
            else
            {
                try
                {
                    BookingAdmin = await _bookingRepository.GetAllBookingsbyId((int)id);
                    if (BookingAdmin == null)
                    {
                        return NotFound();
                    }
                }
                catch
                {
                    throw new Exception();
                }
                return Page();
            }
        }
    }
}
