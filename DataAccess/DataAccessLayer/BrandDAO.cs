using BuildObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessLayer
{
    public class BrandDAO : BaseDao<BrandEntity>
    {
        private readonly ApplicationDbContext _context;

        public BrandDAO() => _context = new ApplicationDbContext();
    }
}
