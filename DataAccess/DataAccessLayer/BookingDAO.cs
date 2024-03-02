using BuildObject.Entities;
using DataAccess.DataAccessLayer;
using DataAccess.DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookingDAO : BaseDao<BookingEntity>
    {
        private static BookingDAO instance = null;
        private static readonly object instanceLock = new object();
        private BookingDAO() { }

        public static BookingDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingDAO();
                    }
                }
                return instance;
            }
        }

        public async Task<IList<BookingEntity>> GetAllBookingAsync()
        {
            var _dbContext = new ApplicationDbContext();
            return await _dbContext.Bookings.ToListAsync();
        }
    }
}
