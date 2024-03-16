using BuildObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccessLayer
{
    public class BookingDAO : BaseDao<BookingEntity>
    {
        private readonly ApplicationDbContext _context;

        public BookingDAO() => _context = new ApplicationDbContext();
        public async Task InsertNewBooking(BookingEntity entity)
        {
            try
            {
                await _context.Set<BookingEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}" + ex);
            }
        }

        public List<BookingEntity> GetBookingsForCar(int carID)
        {
            List<BookingEntity> result;
            try
            {
                var context = new ApplicationDbContext();
                result = context.Bookings.Where(x => x.CarId == carID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
