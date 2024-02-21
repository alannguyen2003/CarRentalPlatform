using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildObject.Entities.Abstract;

namespace BuildObject.Entities;

[Table("Status")]
public class CarStatusEntity : BaseEntity
{
    [StringLength(255)] 
    public string StatusName { get; set; } = null!;

    public ICollection<CarEntity> Cars { get; set; }
}