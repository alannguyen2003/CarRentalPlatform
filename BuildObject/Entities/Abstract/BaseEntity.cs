using System.ComponentModel.DataAnnotations;

namespace BuildObject.Entities.Abstract;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}