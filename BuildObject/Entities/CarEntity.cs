using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildObject.Entities.Abstract;

namespace BuildObject.Entities;

[Table("Cars")]
public class CarEntity : BaseEntity
{
    [StringLength(100)] 
    public string Model { get; set; } = null!;

    [StringLength(50)] 
    public string LicensePlate { get; set; } = null!;
    
    public int Status { get; set; }
    
    [ForeignKey("BrandId")]
    public int BrandId { get; set; }
    public virtual BrandEntity Brand { get; set; }
    
    [ForeignKey("LocationId")]
    public int LocationId { get; set; }
    public virtual LocationEntity Location { get; set; }
    
    public ICollection<BookingEntity> Bookings { get; set; }
}