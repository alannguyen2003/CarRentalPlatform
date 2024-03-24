using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using BusinessObject.Entities;
using DataAccess.DataAccessLayer;

namespace CarRentalPlatform.Pages.AdminPage.AccountGenerate
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.DataAccessLayer.ApplicationDbContext _context;

        public IndexModel(DataAccess.DataAccessLayer.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CarEntity> CarEntity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Cars != null)
            {
                CarEntity = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Location).ToListAsync();
            }
        }
    }
}
