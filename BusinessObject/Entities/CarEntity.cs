﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildObject.Entities;
using BuildObject.Entities.Abstract;

namespace BusinessObject.Entities;

[Table("Cars")]
public class CarEntity : BaseEntity
{
    [StringLength(100)] 
    public string Model { get; set; } = null!;

    [StringLength(50)] 
    public string LicensePlate { get; set; } = null!;

    public string ThumbnailImage { get; set; } = null!;
    
    public int PricePerDay { get; set; }
    public int PricePerHour { get; set; }
    public int PricePerMonth { get; set; }
    
    //1 --> Available 
    //2 --> On booking
    //3 --> On checking
    //4 --> Unavailable
    public int Status { get; set; }
    public string Description { get; set; } = null!;
    
    [ForeignKey("BrandId")]
    public int BrandId { get; set; }
    public virtual BrandEntity Brand { get; set; }
    
    [ForeignKey("LocationId")]
    public int LocationId { get; set; }
    public virtual LocationEntity Location { get; set; }
}