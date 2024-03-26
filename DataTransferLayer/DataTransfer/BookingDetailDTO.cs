using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferLayer.DataTransfer
{
    public class BookingDetailDTO
    {
        public int BookingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public string CarModel { get; set; }
        public string CustomerFirstName { get; set; }
        public int Status { get; set; }
        public int DepositAmount { get; set; }
        public int TotalAmount { get; set; }
        public string Note { get; set; }

        public string GetStatusText()
        {
            return Status switch
            {
                1 => "On Booking",
                2 => "Car Takeaway",
                3 => "On Checking",
                4 => "Done",
                5 => "Cancel",
                6 => "Paying",
                _ => "Unknown"
            };
        }

        public string GetStatusClass()
        {
            return Status switch
            {
                1 => "bg-warning border border-dark text-white",
                2 => "bg-primary border border-dark text-white",
                3 => "bg-info border border-dark text-white",
                4 => "bg-success border border-dark text-white",
                5 => "bg-danger border border-dark text-white",
                6 => "bg-purple-light border border-dark text-dark",
                _ => "bg-secondary border border-dark"
            };
        }
    }
}
