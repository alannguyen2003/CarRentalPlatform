using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using DataAccess.DataAccessLayer;

namespace CarRentalPlatform.Pages.AdminPage.BookingManagement
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.DataAccessLayer.ApplicationDbContext _context;

        public IndexModel(DataAccess.DataAccessLayer.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BookingEntity> BookingEntity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Bookings != null)
            {
                BookingEntity = await _context.Bookings
                .Include(b => b.Car)
                .Include(b => b.Customer).ToListAsync();
            }
        }
    }
}
