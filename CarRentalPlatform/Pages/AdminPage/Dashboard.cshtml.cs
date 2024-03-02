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

namespace CarRentalPlatform.Pages.AdminPage
{
    public class DashboardModel : PageModel
    {

        private readonly ICarRepository _carRepository = new CarRepository();
        
        private readonly IAccountRepository _accountRepository = new AccountRepository();

        public IList<AccountEntity> AccountEntity { get;set; }
        public IList<CarEntity> CarEntity { get; set; }


        public async Task OnGetAsync()
        {
            var Accounts = await _accountRepository.GetAllAccounts();
            var Cars = await _carRepository.GetAllCars();
            if (Cars != null || Accounts != null)
            {
                AccountEntity = Accounts;
                CarEntity = Cars;
            }
        }
    }
}
