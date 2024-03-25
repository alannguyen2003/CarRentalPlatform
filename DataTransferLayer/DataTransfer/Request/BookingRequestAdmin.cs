using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferLayer.DataTransfer.Request
{
    public class BookingRequestAdmin
    {
        public int BookingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CarModel { get; set; }
        public string CustomerFirstName { get; set; }
        public int DepositAmount { get; set; }
        public int TotalAmount { get; set; }
        public string LicensePlate { get; set; } = null!;
        public string Feedback { get; set; } = null!;
        public string Note { get; set; } = null!;
        public int AccId { get; set; }
        public int CarId { get; set; }
    }
}
