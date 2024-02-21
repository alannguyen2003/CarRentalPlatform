using System.ComponentModel.DataAnnotations.Schema;
using BuildObject.Entities.Abstract;

namespace BuildObject.Entities;

[Table("Contracts")]
public class ContractEntity : BaseEntity
{
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public int DepositAmount { get; set; }
    
    public int TotalAmount { get; set; }
    
    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    public virtual AccountEntity Customer { get; set; } = null!;
    
}