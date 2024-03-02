using BuildObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Abstract
{
    public interface IBookingRepository
    {
        Task CreateBooking(BookingEntity entity);
        Task<BookingEntity?> GetBookingById(int id);
        Task<IList<BookingEntity>> GetAllBookings();
        Task UpdateBooking(BookingEntity entity);
        Task DeleteBooking(BookingEntity entity);
    }
}
