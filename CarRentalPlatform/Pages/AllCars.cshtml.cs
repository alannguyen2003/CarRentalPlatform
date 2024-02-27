using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;


namespace CarRentalPlatform.Pages
{
    public class AllCarsModel : PageModel
    {
        private readonly CarEntityDAO _entityDAO;
        private readonly ApplicationDbContext _context;

        public AllCarsModel(ApplicationDbContext context, CarEntityDAO entityDAO)
        {
            _context = context;
            _entityDAO = entityDAO;
        }
        public IList<CarEntity> CarEntity { get; private set; }

        public async Task OnGetAsync()
        {
            var cars = _entityDAO.GetAll();
            if (cars != null)
            {
                CarEntity = await _entityDAO.GetCarsAsync();
            }
        }
    }
}
