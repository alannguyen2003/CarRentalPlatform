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
        private readonly CarEntityDAO _entityDAO;


        public DeleteModel(CarEntityDAO entityDAO)
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

            var carentity = await _entityDAO.GetCarsByIdAsync(id);

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
            var cars = _entityDAO.GetAll();

            if (id == null || cars == null)
            {
                return NotFound();
            }
            var carentity = await _entityDAO.GetCarsByIdAsync(id);

            if (carentity != null)
            {
                CarEntity = carentity;
                await _entityDAO.DeleteEntity(CarEntity);
            }

            return RedirectToPage("./Index");
        }
    }
}
