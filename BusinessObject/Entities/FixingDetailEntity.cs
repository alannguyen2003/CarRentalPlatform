using BuildObject.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities
{
    [Table("FixingDetails")]
    public class FixingDetailEntity : BaseEntity
    {
        [ForeignKey("BookingId")]
        public int BookingId { get; set; }

        public virtual BookingEntity Booking { get; set; }

        public string FixingDescription { get; set; }

        public int Price { get; set; }
    }
}
