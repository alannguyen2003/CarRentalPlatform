using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using DataAccess.DataAccessLayer;
using DataTransferLayer.DataTransfer.Response;
using Repository.Repository;
using Repository.Repository.Abstract;
using DataTransferLayer.DataTransfer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;

namespace CarRentalPlatform.Pages.AdminPage.BookingManagement
{
    public class IndexModel : PageModel
    {
        private readonly IBookingRepository bookingRepository;

        public IndexModel(IBookingRepository repo)
        {
            bookingRepository = repo;
        }

        public IList<BookingResponse> Booking { get;set; } = default!;
        [BindProperty]
        public string Valuetime { get; set; }
        public async Task OnGetAsync()
        { 
                Booking = await bookingRepository.GetAllBookingsDashBoard();
        }
        public async Task<IActionResult> OnPost()
        {
            string selectedValue = Valuetime;
            if (!string.IsNullOrEmpty(selectedValue))
            {
                Booking = await bookingRepository.GetBookingsByTimeRange(Valuetime);

            }
            else
            {
                Booking = await bookingRepository.GetAllBookingsDashBoard();
            }

            return Page();
        }
    }
}
