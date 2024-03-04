using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using CarRentalPlatform.Configuration;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using Repository.Repository;

namespace CarRentalPlatform.Pages.AdminPage.Car
{
    public class DetailsModel : PageModel
    {
        private readonly ICarRepository _carRepository = new CarRepository();

        public CarEntity? CarEntity { get; set; }

        [BindProperty]
        public bool IsLogin { get; set; }
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
            IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
            return Page();
        }
    }
}
