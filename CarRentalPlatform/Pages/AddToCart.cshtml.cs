using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPlatform.Pages
{
    public class AddToCartModel : PageModel
    {
        private readonly CarEntityDAO _carEntityDAO;

        public AddToCartModel(CarEntityDAO carEntityDAO)
        {
            _carEntityDAO = carEntityDAO;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public CarEntity Car { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Car = await _carEntityDAO.GetCarsByIdAsync(Car.Id);

            if (Car == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
