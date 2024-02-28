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
    public class DetailsModel : PageModel
    {
        private readonly ICarRepository _carRepository;

        public DetailsModel(CarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public CarEntity CarEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var cars = await _carRepository.GetAllCars();
            if (id == null || cars == null)
            {
                return NotFound();
            }

            var carEntity = await _carRepository.GetCarById(id);
            if (carEntity == null)
            {
                return NotFound();
            }
            else 
            {
                CarEntity = carEntity;
            }
            return Page();
        }
    }
}
