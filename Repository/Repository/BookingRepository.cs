using BusinessObject.Entities;
using Repository.Repository.Abstract;
using DataAccess.DataAccessLayer;
using DataTransferLayer.DataTransfer;
using Microsoft.EntityFrameworkCore;
using DataTransferLayer.DataTransfer.Response;
using DataTransferLayer.DataTransfer.Request;

namespace Repository.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDAO _bookingDao;
        private readonly CarDAO _carDao;

        public BookingRepository()
        {
            _bookingDao = new BookingDAO();
            _carDao = new CarDAO();
        }
        public async Task<List<BookingEntity>> GetAllBookings() => await _bookingDao.GetAll().Result.ToListAsync();
        public async Task<List<BookingEntity>> GetBookingsByCustomerId(int customerID) => _bookingDao.GetBookingsByCustomerId(customerID);
		public async Task CreateBooking(BookingRequest request)
        {
            BookingEntity entity = new BookingEntity()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Note = request.Note,
                Status = 1,
                Feedback = "",
                CustomerId = request.CustomerId,
                CarId = request.CarId,
                DepositAmount = request.DepositAmount
            };
            CarEntity car = _carDao.GetEntityById(request.CarId).Result;
            if (car != null)
            {
                car.Status = 2;
                await _carDao.UpdateEntity(car);
            }
            await _bookingDao.InsertNewBooking(entity);
        }

        // In BookingRepository.cs
        public async Task UpdateBookingStatus(int bookingId, int newStatus)
        {
            var booking = await _bookingDao.GetEntityById(bookingId);
            if (booking != null)
            {
                booking.Status = newStatus;
                await _bookingDao.UpdateEntity(booking);
            }
        }

        public Task<BookingEntity?> GetBookingById(int id) => _bookingDao.GetEntityById(id);
        public Task UpdateBooking(BookingEntity entity) => _bookingDao.UpdateEntity(entity);
        public Task DeleteBooking(BookingEntity entity) => _bookingDao.DeleteEntity(entity);
        public List<BookingEntity> GetBookingsForCar(int carID) => _bookingDao.GetBookingsForCar(carID);
        public async Task<List<BookingDetailDTO>> GetAllBookingDetails() => await _bookingDao.GetAllBookingDetails();
        public async Task<BookingDetailDTO> GetBookingDetailsById(int bookingId) => await _bookingDao.GetBookingDetailsById(bookingId);
        public async Task<List<BookingDetailDTO>> GetBookingDetailsByCustomerID(int customerID) => await _bookingDao.GetBookingDetailsByCustomerID(customerID);

        public async Task<List<BookingResponse>> GetBookingsByTimeRange(string timeRange) => await _bookingDao.GetBookingsByTimeRange(timeRange);
        public async Task<List<BookingResponse>> GetBookingByStatus(int Status) => await _bookingDao.GetBookingByStatus(Status);
        public async Task<List<BookingResponse>> GetBookingByPrice(int price) => await _bookingDao.GetBookingByPrice(price);
        public async Task<List<BookingResponse>> GetBookingByDay(DateTime? day) => await _bookingDao.GetBookingByDay(day);
        public async Task<List<BookingResponse>> GetAllBookingsDashBoard() =>await _bookingDao.GetAllBookingsDashBoard();
        public async Task<BookingRequestAdmin> GetAllBookingsbyId(int id) => await _bookingDao.GetAllBookingsbyId(id);

    }
}
