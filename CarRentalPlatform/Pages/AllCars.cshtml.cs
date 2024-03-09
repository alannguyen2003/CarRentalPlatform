using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;


namespace CarRentalPlatform.Pages
{
    public class AllCarsModel : PageModel
    {
        private readonly ICarRepository _carRepo;

        public AllCarsModel(ICarRepository carRepo)
        {
            _carRepo = carRepo;
        }
        public IList<CarEntity> CarEntity { get; private set; }

        public async Task OnGetAsync()
        {
            var cars = _carRepo.GetAllCars();
            if (cars != null)
            {
                CarEntity = await _carRepo.GetAllCars();
            }
            
        }
    }
}
