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
    public class IndexModel : PageModel
    {
        private readonly ICarRepository _carRepository;

        public IndexModel(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        [BindProperty]
        public List<CarEntity> CarEntity { get;set; }
        [BindProperty]
        public bool IsLogin { get; set; }

        public async Task OnGetAsync()
        {
            var cars = _carRepository.GetAllCars();
            if (cars != null)
            {
                CarEntity = await cars;
            }
            IsLogin = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isLogin");

        }
    }
}
