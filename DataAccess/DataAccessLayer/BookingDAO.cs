using BuildObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccessLayer
{
    public class BookingDAO : BaseDao<BookingEntity>
    {
        private readonly ApplicationDbContext _context;

        public BookingDAO() => _context = new ApplicationDbContext();
    }
}
