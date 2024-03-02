using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class BrandRepository : IBrandRepository
    {
        public Task CreateBrand(BrandEntity entity) => BrandDAO.Instance.Create(entity);
        public Task<IList<BrandEntity>> GetAllBrands() => BrandDAO.Instance.GetAllBrandAsync();
        public Task<BrandEntity?> GetBrandById(int id) => BrandDAO.Instance.GetEntityById(id);
        public Task UpdateBrand(BrandEntity entity) => BrandDAO.Instance.UpdateEntity(entity);
        public Task DeleteAccount(BrandEntity entity) => BrandDAO.Instance.DeleteEntity(entity);
    }
}
