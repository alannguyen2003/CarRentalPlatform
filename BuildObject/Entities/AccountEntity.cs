using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildObject.Entities.Abstract;

namespace BuildObject.Entities;

[Table("Accounts")]
public class AccountEntity : BaseEntity
{
    [Required]
    [StringLength(100)] 
    public string? Email { get; set; }
    
    [Required]
    [StringLength(100)]
    public string? FullName { get; set; }
    
    [Required]
    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(20)] 
    public string PhoneNumber { get; set; }
    
    public bool Gender { get; set; }
    
    [Required]
    [StringLength(50)]
    public string? Password { get; set; }

    public int WalletBalance { get; set; }
    
    [ForeignKey("RoleId")]
    public int RoleId { get; set; }

    public virtual RoleEntity Role { get; set; }
    
    public ICollection<DriverLicenseEntity> DriverLicenses { get; set; }
}