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
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.DataAccessLayer.ApplicationDbContext _context;

        public DetailsModel(DataAccess.DataAccessLayer.ApplicationDbContext context)
        {
            _context = context;
        }

      public CarEntity CarEntity { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var carentity = await _context.Cars.FirstOrDefaultAsync(m => m.Id == id);
            if (carentity == null)
            {
                return NotFound();
            }
            else 
            {
                CarEntity = carentity;
            }
            return Page();
        }
    }
}
