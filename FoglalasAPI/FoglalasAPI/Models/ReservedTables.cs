using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FoglalasAPI.Models
{
    [PrimaryKey("Reservation", "Table")]
    public class ReservedTables
    {
        [Required]
        [ForeignKey("ReservationFK")]
        public int Reservation { get; set; }
        [Required]
        [ForeignKey("TableFK")]
        public int Table { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
