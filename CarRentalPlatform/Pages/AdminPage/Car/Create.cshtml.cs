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
        private readonly BrandDAO _brandDAO;
        private readonly LocationDAO _locationDAO;

        public CreateModel(CarEntityDAO carEntityDAO,LocationDAO locationDAO,BrandDAO brandDAO)
        {
            _entityDAO = carEntityDAO;
            _brandDAO = brandDAO;
            _locationDAO = locationDAO;
        }
         
        public async Task<IActionResult> OnGet()
        {
            var brands = await _brandDAO.GetAll();
            var location = await _locationDAO.GetAll();
        ViewData["BrandId"] = new SelectList(brands, "Id", "BrandName");
        ViewData["LocationId"] = new SelectList(location, "Id", "Address");
            return Page();
        }

        [BindProperty]
        public CarEntity CarEntity { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           /* var cars = await _entityDAO.GetCarsAsync();
          if (cars == null || CarEntity == null)
            {
                return Page();
            }*/

            await _entityDAO.Create(CarEntity);

            return RedirectToPage("./Index");
        }
    }
}
