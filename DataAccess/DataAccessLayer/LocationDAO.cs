using BuildObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Entities;

namespace DataAccess.DataAccessLayer
{
    public class LocationDAO : BaseDao<LocationEntity>
    {
        private readonly ApplicationDbContext _context;

        public LocationDAO() => _context = new ApplicationDbContext();
    }
}
