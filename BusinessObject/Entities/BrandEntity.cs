using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildObject.Entities.Abstract;

namespace BusinessObject.Entities;

[Table("Brands")]
public class BrandEntity : BaseEntity
{
    [StringLength(100)] 
    public string BrandName { get; set; } = null!;

    public ICollection<CarEntity> Cars { get; set; }
}