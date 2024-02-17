using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FoglalasAPI.Models
{
    [PrimaryKey("Restauarnt", "Table")]
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
