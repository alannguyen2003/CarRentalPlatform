using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildObject.Entities.Abstract;

namespace BuildObject.Entities;

[Table("Locations")]
public class LocationEntity : BaseEntity
{
    [StringLength(255)] 
    public string Address { get; set; } = null!;

    public ICollection<CarEntity> Cars { get; set; }
    
}