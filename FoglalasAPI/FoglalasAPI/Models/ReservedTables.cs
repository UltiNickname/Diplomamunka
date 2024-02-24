using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FoglalasAPI.Models
{
    [Keyless]
    public class ReservedTables
    {
        [Required]
        public int ReservationId { get; set; }
        [Key, ForeignKey("ReservationId")]
        public virtual Reservation Reservation { get; set; }
        [Required]
        public int TableId { get; set; }
        [Key, ForeignKey("TableId")]
        public virtual Table Table { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
