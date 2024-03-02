using BuildObject.Entities;
using DataAccess;
using Repository.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class BookingRepository : IBookingRepository
    {
        public Task CreateBooking(BookingEntity entity) => BookingDAO.Instance.Create(entity);
        public Task<IList<BookingEntity>> GetAllBookings() => BookingDAO.Instance.GetAllBookingAsync();
        public Task<BookingEntity?> GetBookingById(int id) => BookingDAO.Instance.GetEntityById(id);
        public Task UpdateBooking(BookingEntity entity) => BookingDAO.Instance.UpdateEntity(entity);
        public Task DeleteBooking(BookingEntity entity) => BookingDAO.Instance.DeleteEntity(entity);
    }
}
