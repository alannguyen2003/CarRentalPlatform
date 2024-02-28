using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPlatform.Pages
{
    public class AddToCartModel : PageModel
    {
        private readonly CarEntityDAO _entityDAO;

        public AddToCartModel(CarEntityDAO entityDAO)
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
