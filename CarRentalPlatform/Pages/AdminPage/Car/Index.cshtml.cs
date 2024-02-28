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
using Repository.Repository;

namespace CarRentalPlatform.Pages.AdminPage.Car
{
    public class IndexModel : PageModel
    {
        private readonly ICarRepository _carRepository;

        public IndexModel(CarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public IList<CarEntity> CarEntity { get;set; }

        public async Task OnGetAsync()
        {
            var cars = _carRepository.GetAllCars();
            if (cars != null)
            {
                CarEntity = await _carRepository.GetAllCars();
            }
        }
    }
}
