﻿using BuildObject.Entities;
using BusinessObject.Entities;
using DataAccess.DataAccessLayer.Abstract;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.DataTransfer.Request;
using DataTransferLayer.DataTransfer.Response;
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
                    ActualReturnDate = b.ActualReturnDate,
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
                    ActualReturnDate = b.ActualReturnDate,
                    CarModel = b.Car.Model,
                    CustomerFirstName = b.Customer.FirstName + (b.Customer.LastName != null ? " " + b.Customer.LastName : ""),
                    Status = b.Status,
                    Note = b.Note,
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
                    ActualReturnDate = b.ActualReturnDate,
                    CarModel = b.Car.Model,
                    CustomerFirstName = b.Customer.FirstName + (b.Customer.LastName != null ? " " + b.Customer.LastName : ""),
                    Status = b.Status,
                    DepositAmount = b.DepositAmount,
                    TotalAmount = b.TotalAmount
                }).ToListAsync();

            return bookingDetails;
        }



        public async Task<List<BookingResponse>> GetBookingsByTimeRange(string timeRange)
        {
            try
            {
                DateTime startDate;

                switch (timeRange.ToLower())
                {
                    case "day":
                        startDate = DateTime.Now.AddDays(-5);
                        break;
                    case "month":
                        startDate = DateTime.Now.AddMonths(-1);
                        break;
                    case "year":
                        startDate = DateTime.Now.AddYears(-1);
                        break;
                    default:
                        throw new Exception("Invalid time range. Supported values are 'day', 'month', and 'year'.");
                }


                var filteredTransactions = await _context.Bookings.Where(t => t.StartDate >= startDate)
                    .Include(b => b.Car)
                    .Include(b => b.Customer)
                    .Select(b => new BookingResponse
                    {
                        BookingId = b.Id,
                        StartDate = b.StartDate,
                        EndDate = b.EndDate,
                        CarModel = b.Car.Model,
                        CustomerFirstName = b.Customer.FirstName + (b.Customer.LastName != null ? " " + b.Customer.LastName : ""),
                        Status = b.Status,
                        DepositAmount = b.DepositAmount,
                        TotalAmount = b.TotalAmount,
                        LicensePlate = b.Car.LicensePlate,
                        Feedback = b.Feedback,
                        Note = b.Note,
                    })
                    .OrderByDescending(o => o.StartDate)
                    .ToListAsync();

                return filteredTransactions;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<BookingResponse>> GetBookingByStatus(int Status)
        {
            var bookingDetails = await _context.Bookings
                .Where(b => b.Status == Status)
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .Select(b => new BookingResponse
                {
                    BookingId = b.Id,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    CarModel = b.Car.Model,
                    CustomerFirstName = b.Customer.FirstName + (b.Customer.LastName != null ? " " + b.Customer.LastName : ""),
                    Status = b.Status,
                    DepositAmount = b.DepositAmount,
                    TotalAmount = b.TotalAmount,
                    LicensePlate = b.Car.LicensePlate,
                    Feedback = b.Feedback,
                    Note = b.Note,
                })
                .OrderByDescending(o => o.StartDate)
                .ToListAsync();

            return bookingDetails;
        }
        public async Task<List<BookingResponse>> GetBookingByPrice(int price)
        {
            var bookingDetails = await _context.Bookings
                .Where(b => b.TotalAmount >= price)
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .Select(b => new BookingResponse
                {
                    BookingId = b.Id,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    CarModel = b.Car.Model,
                    CustomerFirstName = b.Customer.FirstName + (b.Customer.LastName != null ? " " + b.Customer.LastName : ""),
                    Status = b.Status,
                    DepositAmount = b.DepositAmount,
                    TotalAmount = b.TotalAmount,
                    LicensePlate = b.Car.LicensePlate,
                    Feedback = b.Feedback,
                    Note = b.Note,
                })
                .OrderByDescending(o => o.StartDate)
                .ToListAsync();

            return bookingDetails;
        }
        public async Task<List<BookingResponse>> GetBookingByDay(DateTime? day)
        {
            var bookingDetails = await _context.Bookings
                .Where(o => o.StartDate >= day.Value.Date)
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .Select(b => new BookingResponse
                {
                    BookingId = b.Id,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    CarModel = b.Car.Model,
                    CustomerFirstName = b.Customer.FirstName + (b.Customer.LastName != null ? " " + b.Customer.LastName : ""),
                    Status = b.Status,
                    DepositAmount = b.DepositAmount,
                    TotalAmount = b.TotalAmount,
                    LicensePlate = b.Car.LicensePlate,
                    Feedback = b.Feedback,
                    Note = b.Note,
                })
                .OrderByDescending(o => o.StartDate)
                .ToListAsync();

            return bookingDetails;
        }

        public async Task<List<BookingResponse>> GetAllBookingsDashBoard()
        {
            var bookingDetails = await _context.Bookings
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .Select(b => new BookingResponse
                {
                    BookingId = b.Id,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    CarModel = b.Car.Model,
                    CustomerFirstName = b.Customer.FirstName + (b.Customer.LastName != null ? " " + b.Customer.LastName : ""),
                    Status = b.Status,
                    DepositAmount = b.DepositAmount,
                    TotalAmount = b.TotalAmount,
                    LicensePlate = b.Car.LicensePlate,
                    Feedback = b.Feedback,
                    Note = b.Note,
                })
                .OrderByDescending(o => o.StartDate)
                .ToListAsync();

            return bookingDetails;
        }
        public async Task<BookingRequestAdmin> GetAllBookingsbyId(int id)
        {
            var bookingDetails = await _context.Bookings
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .Select(b => new BookingRequestAdmin
                {
                    BookingId = b.Id,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    CarModel = b.Car.Model,
                    CustomerFirstName = b.Customer.FirstName + (b.Customer.LastName != null ? " " + b.Customer.LastName : ""),
                    DepositAmount = b.DepositAmount,
                    TotalAmount = b.TotalAmount,
                    LicensePlate = b.Car.LicensePlate,
                    Feedback = b.Feedback,
                    Note = b.Note,
                    AccId = b.CustomerId,
                    CarId = b.CarId
                })
                .SingleOrDefaultAsync(b => b.BookingId == id);

            return bookingDetails;
        }
    }
   

}
