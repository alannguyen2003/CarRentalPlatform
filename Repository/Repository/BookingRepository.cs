using BuildObject.Entities;
using DataAccess;
using Repository.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccessLayer;
using DataTransferLayer.DataTransfer;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDAO _bookingDao;

        public BookingRepository()
        {
            _bookingDao = new BookingDAO();
        }
        public async Task<List<BookingEntity>> GetAllBookings() => await _bookingDao.GetAll().Result.ToListAsync();
        public async Task CreateBooking(BookingRequest request)
        {
            BookingEntity entity = new BookingEntity()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Note = request.Note,
                Feedback = "",
                CustomerId = request.CustomerId,
                CarId = request.CarId,
                DepositAmount = request.DepositAmount
            };
            await _bookingDao.InsertNewBooking(entity);
        }

        public Task<BookingEntity?> GetBookingById(int id) => _bookingDao.GetEntityById(id);
        public Task UpdateBooking(BookingEntity entity) => _bookingDao.UpdateEntity(entity);
        public Task DeleteBooking(BookingEntity entity) => _bookingDao.DeleteEntity(entity);
    }
}
