using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPlatform.Pages.BookCar
{
    public class AllCarsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AllCarsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CarEntity> Cars { get; set; }

        public void OnGet()
        {
            Cars = _context.Cars
                            .Include(c => c.Brand)
                            .Include(c => c.Location)
                            .ToList();
        }
    }
}
