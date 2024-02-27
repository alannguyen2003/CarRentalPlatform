using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;

namespace CarRentalPlatform.Pages.AdminPage.Car
{
    public class CreateModel : PageModel
    {
        private readonly CarEntityDAO _entityDAO;

        public CreateModel(CarEntityDAO carEntityDAO)
        {
            _entityDAO = carEntityDAO;
        }

        public async Task<IActionResult> OnGet()
        {
            var entityCar = await _entityDAO.GetCarsAsync();
        ViewData["BrandId"] = new SelectList(entityCar.Select(c => c.Brand), "Id", "BrandName");
        ViewData["LocationId"] = new SelectList(entityCar.Select(c => c.Location), "Id", "Address");
            return Page();
        }

        [BindProperty]
        public CarEntity CarEntity { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var cars = await _entityDAO.GetCarsAsync();
          if (!ModelState.IsValid || cars == null || CarEntity == null)
            {
                return Page();
            }

            await _entityDAO.Create(CarEntity);

            return RedirectToPage("./Index");
        }
    }
}
