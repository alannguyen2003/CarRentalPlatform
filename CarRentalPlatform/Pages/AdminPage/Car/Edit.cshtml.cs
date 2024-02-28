using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using Repository.Repository;

namespace CarRentalPlatform.Pages.AdminPage.Car
{
    public class EditModel : PageModel
    {
        private readonly ICarRepository _carRepository;


        public EditModel(CarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [BindProperty]
        public CarEntity CarEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var cars = _carRepository.GetAllCars();
            if (id == null || cars == null)
            {
                return NotFound();
            }

            var carentity =  await _carRepository.GetCarById(id);
            if (carentity == null)
            {
                return NotFound();
            }
            CarEntity = carentity;
            ViewData["BrandId"] = new SelectList(new List<BrandEntity> { carentity.Brand }, "Id", "BrandName");
            ViewData["LocationId"] = new SelectList(new List<LocationEntity> { carentity.Location }, "Id", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           /* if (!ModelState.IsValid)
            {
                return Page();
            }*/

            try
            {
                await _carRepository.UpdateCar(CarEntity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await CarEntityExists(CarEntity.Id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> CarEntityExists(int id)
        {
          return (await _carRepository.GetCarById(id)) != null;
        }
    }
}
