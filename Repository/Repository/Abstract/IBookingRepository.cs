using BuildObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferLayer.DataTransfer;

namespace Repository.Repository.Abstract
{
    public interface IBookingRepository
    {
        Task CreateBooking(BookingRequest request);
        Task<BookingEntity?> GetBookingById(int id);
        Task<List<BookingEntity>> GetAllBookings();
        Task UpdateBooking(BookingEntity entity);
        Task DeleteBooking(BookingEntity entity);
        List<BookingEntity> GetBookingsForCar(int carID);
        Task<List<BookingEntity>> GetBookingsByCustomerId(int customerID);

	}
}
