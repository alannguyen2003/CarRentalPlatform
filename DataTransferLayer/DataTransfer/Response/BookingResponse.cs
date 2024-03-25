using DataTransferLayer.Page;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferLayer.DataTransfer.Response
{
    public class BookingResponse
    {
        public int BookingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CarModel { get; set; }
        public string CustomerFirstName { get; set; }
        public int Status { get; set; }
        public int DepositAmount { get; set; }
        public int TotalAmount { get; set; }
        public string LicensePlate { get; set; } = null!;
        public string Feedback { get; set; } = null!;
        public string Note { get; set; } = null!;


    }
}
