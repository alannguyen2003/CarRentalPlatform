using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using Repository.Repository;

namespace CarRentalPlatform.Pages.AdminPage.Car
{
    public class CreateModel : PageModel
    {
        private readonly ICarRepository _carRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ILocationRepository _locationRepository;

        public CreateModel(CarRepository carRepository, BrandRepository brandRepository, LocationRepository locationRepository)
        {
            _carRepository = carRepository;
            _brandRepository = brandRepository;
            _locationRepository = locationRepository;
        }
         
        public async Task<IActionResult> OnGet()
        {
            var brands = await _brandRepository.GetAllBrands();
            var location = await _locationRepository.GetAllLocations();
            ViewData["BrandId"] = new SelectList(brands, "Id", "BrandName");
            ViewData["LocationId"] = new SelectList(location, "Id", "Address");
            return Page();
        }

        [BindProperty]
        public CarEntity CarEntity { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           /* var cars = await _entityDAO.GetCarsAsync();
          if (cars == null || CarEntity == null)
            {
                return Page();
            }*/

            await _carRepository.CreateCar(CarEntity);

            return RedirectToPage("./Index");
        }
    }
}
