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
        private static BrandDAO instance = null;
        private static readonly object instanceLock = new object();
        private BrandDAO() { }

        public static BrandDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BrandDAO();
                    }
                }
                return instance;
            }
        }

        public async Task <IList<BrandEntity>> GetAllBrandAsync()
        {
            try
            {
                var _dbContext = new ApplicationDbContext();
                return await _dbContext.Brands.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}" + ex);
            }
        }
    }
}
