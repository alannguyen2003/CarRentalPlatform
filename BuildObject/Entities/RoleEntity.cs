using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildObject.Entities.Abstract;

namespace BuildObject.Entities;

[Table("Roles")]
public class RoleEntity : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string? RoleName { get; set; }
    
    public ICollection<AccountEntity> Accounts { get; set; }
}