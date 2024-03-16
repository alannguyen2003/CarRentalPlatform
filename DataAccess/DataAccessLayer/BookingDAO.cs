using BuildObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using DataTransferLayer.DataTransfer;
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

        public List<BookingEntity> GetBookingsByCustomerId(int customerID)
		{
			List<BookingEntity> result;
			try
			{
				var context = new ApplicationDbContext();
				result = context.Bookings.Where(x => x.CustomerId == customerID).ToList();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
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

        public async Task<List<BookingDetailDTO>> GetAllBookingDetails()
        {
            var bookingDetails = await _context.Bookings
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .Select(b => new BookingDetailDTO
                {
                    BookingId = b.Id,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    CarModel = b.Car.Model,
                    CustomerFirstName = b.Customer.FirstName + (b.Customer.LastName != null ? " " + b.Customer.LastName : ""),
                    Status = b.Status,
                    DepositAmount = b.DepositAmount,
                    TotalAmount = b.TotalAmount
                }).ToListAsync();

            return bookingDetails;
        }

        public async Task<BookingDetailDTO> GetBookingDetailsById(int bookingId)
        {
            var bookingDetail = await _context.Bookings
                .Where(b => b.Id == bookingId)
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .Select(b => new BookingDetailDTO
                {
                    BookingId = b.Id,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    CarModel = b.Car.Model,
                    CustomerFirstName = b.Customer.FirstName + (b.Customer.LastName != null ? " " + b.Customer.LastName : ""),
                    Status = b.Status,
                    DepositAmount = b.DepositAmount,
                    TotalAmount = b.TotalAmount
                }).FirstOrDefaultAsync();

            return bookingDetail;
        }

        public async Task<List<BookingDetailDTO>> GetBookingDetailsByCustomerID(int customerID)
        {
            var bookingDetails = await _context.Bookings
                .Where(b => b.CustomerId == customerID)
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .Select(b => new BookingDetailDTO
                {
                    BookingId = b.Id,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    CarModel = b.Car.Model,
                    CustomerFirstName = b.Customer.FirstName + (b.Customer.LastName != null ? " " + b.Customer.LastName : ""),
                    Status = b.Status,
                    DepositAmount = b.DepositAmount,
                    TotalAmount = b.TotalAmount
                }).ToListAsync();

            return bookingDetails;
        }

    }
}
