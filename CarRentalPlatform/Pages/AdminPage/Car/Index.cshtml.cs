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
    public class IndexModel : PageModel
    {
        private readonly CarEntityDAO _entityDAO;

        public IndexModel(CarEntityDAO entityDAO)
        {
            _entityDAO = entityDAO;
        }

        public IList<CarEntity> CarEntity { get;set; } = default!;

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
