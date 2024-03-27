using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Abstract
{
    public interface IFixingDetailRepository
    {
        Task CreateFixingDetail(FixingDetailEntity entity);

        Task<List<FixingDetailEntity>> GetFixingDetailsByBookingId(int bookingID);

    }
}
