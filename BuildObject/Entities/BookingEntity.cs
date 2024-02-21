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
    
    [ForeignKey("ContractId")]
    public int ContractId { get; set; }
    public virtual ContractEntity Contract { get; set; } = null!;

    [ForeignKey("CarId")]
    public int CarId { get; set; }
    public virtual CarEntity Car { get; set; }
}