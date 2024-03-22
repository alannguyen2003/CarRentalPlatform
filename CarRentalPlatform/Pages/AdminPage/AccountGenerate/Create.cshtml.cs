using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;

namespace CarRentalPlatform.Pages.AdminPage.AccountGenerate
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.DataAccessLayer.ApplicationDbContext _context;

        public CreateModel(DataAccess.DataAccessLayer.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "BrandName");
        ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Address");
            return Page();
        }

        [BindProperty]
        public CarEntity CarEntity { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Cars == null || CarEntity == null)
            {
                return Page();
            }

            _context.Cars.Add(CarEntity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
