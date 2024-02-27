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
    public class DetailsModel : PageModel
    {
        private readonly CarEntityDAO _entityDAO;

        public DetailsModel( CarEntityDAO entityDAO)
        {
            _entityDAO = entityDAO;
        }

        public CarEntity CarEntity { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var cars = await _entityDAO.GetAll();
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
    }
}
