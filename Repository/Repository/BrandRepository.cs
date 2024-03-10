using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly BrandDAO _brandDao;

        public BrandRepository()
        {
            _brandDao = new BrandDAO();
        }
        public Task CreateBrand(BrandEntity entity) => _brandDao.Create(entity);
        public async Task<List<BrandEntity>> GetAllBrands() => await _brandDao.GetAll().Result.ToListAsync();
        public Task<BrandEntity?> GetBrandById(int id) => _brandDao.GetEntityById(id);
        public Task UpdateBrand(BrandEntity entity) => _brandDao.UpdateEntity(entity);
        public Task DeleteAccount(BrandEntity entity) => _brandDao.DeleteEntity(entity);
    }
}
