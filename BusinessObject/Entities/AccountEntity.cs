using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildObject.Entities.Abstract;

namespace BusinessObject.Entities;

[Table("Accounts")]
public class AccountEntity : BaseEntity
{
    [Required]
    [StringLength(100)] 
    public string? Email { get; set; }
    
    [Required]
    [StringLength(100)]
    public string? FirstName { get; set; }
    
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
    
    //1 -- ADMIN
    //2 -- EMPLOYEE
    //3 -- CUSTOMER
    public int Role { get; set; }
    
    [StringLength(12)]
    public string DriverLicense { get; set; }
}