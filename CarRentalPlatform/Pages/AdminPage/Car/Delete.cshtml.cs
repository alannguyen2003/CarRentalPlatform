using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;

namespace CarRentalPlatform.Pages.AdminPage.Car
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.DataAccessLayer.ApplicationDbContext _context;

        public DeleteModel(DataAccess.DataAccessLayer.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }
            var carentity = await _context.Cars.FindAsync(id);

            if (carentity != null)
            {
                CarEntity = carentity;
                _context.Cars.Remove(CarEntity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
