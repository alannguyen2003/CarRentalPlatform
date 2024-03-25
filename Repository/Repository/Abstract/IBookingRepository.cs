using BuildObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Entities;
using DataTransferLayer.DataTransfer;
using DataTransferLayer.DataTransfer.Response;
using DataTransferLayer.DataTransfer.Request;

namespace Repository.Repository.Abstract
{
    public interface IBookingRepository
    {
        Task CreateBooking(BookingRequest request);
        Task<BookingEntity?> GetBookingById(int id);
        Task<List<BookingEntity>> GetAllBookings();
        Task UpdateBooking(BookingEntity entity);
        Task DeleteBooking(BookingEntity entity);
        Task UpdateBookingStatus(int bookingId, int newStatus);
        List<BookingEntity> GetBookingsForCar(int carID);
        Task<List<BookingEntity>> GetBookingsByCustomerId(int customerID);
        Task<List<BookingDetailDTO>> GetAllBookingDetails();
        Task<BookingDetailDTO> GetBookingDetailsById(int bookingId);
        Task<List<BookingDetailDTO>> GetBookingDetailsByCustomerID(int customerID);


        Task<List<BookingResponse>> GetBookingsByTimeRange(string timeRange);
        Task<List<BookingResponse>> GetBookingByStatus(int Status);
        Task<List<BookingResponse>> GetBookingByPrice(int price);
        Task<List<BookingResponse>> GetBookingByDay(DateTime? day);
        Task<List<BookingResponse>> GetAllBookingsDashBoard();
        Task<BookingRequestAdmin> GetAllBookingsbyId(int id);
    }
}
