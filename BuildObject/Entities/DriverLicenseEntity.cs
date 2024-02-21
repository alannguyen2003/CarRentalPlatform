using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildObject.Entities;
using BuildObject.Entities.Abstract;

namespace BuildObject;

[Table("DriverLicense")]
public class DriverLicenseEntity : BaseEntity
{
    [StringLength(10)]
    public string? Type { get; set; }
    
    public DateTime IssueDate { get; set; }
    
    [StringLength(255)]
    public string? PlaceOfIssue { get; set; }
    
    [ForeignKey("AccountId")]
    public int AccountId { get; set; }
    public virtual AccountEntity Account { get; set; }
}