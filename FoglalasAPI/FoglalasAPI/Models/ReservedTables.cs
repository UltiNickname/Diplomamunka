using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoglalasAPI.Models
{
    public class ReservedTables
    {
        [Required]
        [ForeignKey("ReservationFK")]
        public Reservation Reservation { get; set; }
        [Required]
        [ForeignKey("TableFK")]
        public Table Table { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
