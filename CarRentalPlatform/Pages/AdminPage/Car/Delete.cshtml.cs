﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly ICarRepository _carRepository = new CarRepository();

        [BindProperty]
        public CarEntity CarEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var cars = _carRepository.GetAllCars();
            if (id == null || cars == null)
            {
                return NotFound();
            }

            var carentity = await _carRepository.GetCarById(id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var cars = _carRepository.GetAllCars();

            if (id == null || cars == null)
            {
                return NotFound();
            }
            var carentity = await _carRepository.GetCarById(id);

            if (carentity != null)
            {
                CarEntity = carentity;
                await _carRepository.DeleteCar(CarEntity);
            }

            return RedirectToPage("./Index");
        }
    }
}