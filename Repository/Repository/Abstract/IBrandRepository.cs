using BuildObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Abstract
{
    public interface IBrandRepository
    {
        Task CreateBrand(BrandEntity entity);
        Task<BrandEntity?> GetBrandById(int id);
        Task<List<BrandEntity>> GetAllBrands();
        Task UpdateBrand(BrandEntity entity);
        Task DeleteAccount(BrandEntity entity);
    }
}
