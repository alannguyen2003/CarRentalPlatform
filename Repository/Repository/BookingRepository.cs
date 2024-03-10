using BuildObject.Entities;
using DataAccess;
using Repository.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccessLayer;
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
        public Task CreateBooking(BookingEntity entity) => _bookingDao.Create(entity);
        public async Task<List<BookingEntity>> GetAllBookings() => await _bookingDao.GetAll().Result.ToListAsync();
        public Task<BookingEntity?> GetBookingById(int id) => _bookingDao.GetEntityById(id);
        public Task UpdateBooking(BookingEntity entity) => _bookingDao.UpdateEntity(entity);
        public Task DeleteBooking(BookingEntity entity) => _bookingDao.DeleteEntity(entity);
    }
}
