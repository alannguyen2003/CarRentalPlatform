using BusinessObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessLayer
{
    public class FixingDetailDAO : BaseDao <FixingDetailEntity>
    {
        private readonly ApplicationDbContext _context;

        public List<FixingDetailEntity> GetFixingDetailssByBookingId(int bookingID)
        {
            List<FixingDetailEntity> result;
            try
            {
                var context = new ApplicationDbContext();
                result = context.FixingDetails.Where(x => x.BookingId == bookingID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
