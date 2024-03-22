using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;

namespace CarRentalPlatform.Pages.AdminPage.AccountGenerate
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.DataAccessLayer.ApplicationDbContext _context;

        public EditModel(DataAccess.DataAccessLayer.ApplicationDbContext context)
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

            var carentity =  await _context.Cars.FirstOrDefaultAsync(m => m.Id == id);
            if (carentity == null)
            {
                return NotFound();
            }
            CarEntity = carentity;
           ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "BrandName");
           ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CarEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarEntityExists(CarEntity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CarEntityExists(int id)
        {
          return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
