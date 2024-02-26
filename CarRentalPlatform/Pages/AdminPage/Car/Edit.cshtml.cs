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

namespace CarRentalPlatform.Pages.AdminPage.Car
{
    public class EditModel : PageModel
    {
        private readonly CarEntityDAO _entityDAO;


        public EditModel(CarEntityDAO entityDAO)
        {
            _entityDAO = entityDAO;
        }

        [BindProperty]
        public CarEntity CarEntity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var cars = _entityDAO.GetAll();
            if (id == null || cars == null)
            {
                return NotFound();
            }

            var carentity =  await _entityDAO.GetCarsByIdAsync(id);
            if (carentity == null)
            {
                return NotFound();
            }
            CarEntity = carentity;
            ViewData["BrandId"] = new SelectList(new List<BrandEntity> { carentity.Brand }, "Id", "BrandName");
            ViewData["LocationId"] = new SelectList(new List<LocationEntity> { carentity.Location }, "Id", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           /* if (!ModelState.IsValid)
            {
                return Page();
            }*/

            try
            {
                await _entityDAO.UpdateEntity(CarEntity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await CarEntityExists(CarEntity.Id)))
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

        private async Task<bool> CarEntityExists(int id)
        {
          return (await _entityDAO.GetEntityById(id)) != null;
        }
    }
}
