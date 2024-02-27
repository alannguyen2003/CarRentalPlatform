using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPlatform.Pages
{
    public class AddToCartModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddToCartModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public CarEntity Car { get; set; }

        public IActionResult OnGet()
        {
            Car = _context.Cars.Include(c => c.Brand).Include(c => c.Location).FirstOrDefault(c => c.Id == Id);

            if (Car == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
