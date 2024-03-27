using BusinessObject.Entities;
using DataAccess.DataAccessLayer;
using Repository.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FixingRepository : IFixingDetailRepository
    {
        private readonly FixingDetailDAO _fixingDetailDao;

        public FixingRepository()
        {
            _fixingDetailDao = new FixingDetailDAO();
        }

        public Task CreateFixingDetail(FixingDetailEntity entity) => _fixingDetailDao.Create(entity);

        public async Task<List<FixingDetailEntity>> GetFixingDetailsByBookingId(int bookingID) => _fixingDetailDao.GetFixingDetailssByBookingId(bookingID);
    }
}
