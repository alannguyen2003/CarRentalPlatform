using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using BuildObject.Entities.Abstract;

namespace BuildObject.Entities;

[Table("Bookings")]
public class BookingEntity : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public DateTime ActualReturnDate { get; set; }

    public string Feedback { get; set; } = null!;

    public string Note { get; set; } = null!;

    //1 --> On Booking
    //2 --> Car Takeaway
    //3 --> On Checking
    //4 --> Done
    public int Status { get; set; }

    public int DepositAmount { get; set; }
    public int TotalAmount { get; set; }
    
    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    public virtual AccountEntity Customer { get; set; } = null!;
    
    [ForeignKey("CarId")]
    public int CarId { get; set; }
    public virtual CarEntity Car { get; set; } = null!;
}