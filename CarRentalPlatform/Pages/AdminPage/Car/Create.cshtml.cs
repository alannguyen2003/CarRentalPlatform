using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildObject.Entities;
using BusinessObject.Entities;
using CarRentalPlatform.Configuration;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using Repository.Repository;

namespace CarRentalPlatform.Pages.AdminPage.Car
{
    public class CreateModel : PageModel
    {
        private readonly ICarRepository _carRepository = new CarRepository();
        private readonly IBrandRepository _brandRepository = new BrandRepository();
        private readonly ILocationRepository _locationRepository = new LocationRepository();
         
        public async Task<IActionResult> OnGet()
        {
            var brands = await _brandRepository.GetAllBrands();
            var location = await _locationRepository.GetAllLocations();
            ViewData["BrandId"] = new SelectList(brands, "Id", "BrandName");
            ViewData["LocationId"] = new SelectList(location, "Id", "Address");
            IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");
            return Page();
        }

        [BindProperty]
        public CarEntity CarEntity { get; set; }
        [BindProperty]
        public bool IsLogin { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           /* var cars = await _entityDAO.GetCarsAsync();
          if (cars == null || CarEntity == null)
            {
                return Page();
            }*/
           CarEntity.ThumbnailImage = "";
            await _carRepository.CreateCar(CarEntity);

            return RedirectToPage("./Index");
        }
    }
}
