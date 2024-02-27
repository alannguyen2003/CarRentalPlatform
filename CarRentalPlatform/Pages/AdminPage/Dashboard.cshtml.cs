using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildObject.Entities;
using DataAccess.DataAccessLayer;

namespace CarRentalPlatform.Pages.AdminPage
{
    public class DashboardModel : PageModel
    {

        private readonly CarEntityDAO _carDAO;
        
        private readonly AccountDao _accountDao;
        public DashboardModel( CarEntityDAO entityDAO, AccountDao accountDao)
        {
            _carDAO = entityDAO;
            _accountDao = accountDao;
        }

        public IList<AccountEntity> AccountEntity { get;set; } = default!;
        public IList<CarEntity> CarEntity { get; set; } = default!;


        public async Task OnGetAsync()
        {
            var Accounts = await _accountDao.GetAccountAsync();
            var Cars = await _carDAO.GetCarsAsync();
            if (Cars != null || Accounts != null)
            {
                AccountEntity = Accounts;
                CarEntity = Cars;
            }
        }
    }
}
