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
        public async Task<IActionResult> OnGet(int? id)
        {
            try
            {
               BookingAdmin = await _bookingRepository.GetAllBookingsbyId((int)id);
                if (BookingAdmin == null)
                {
                    return NotFound();
                }
            }
            catch {
                throw new Exception();
            }
            return Page();
        }
    }
}
