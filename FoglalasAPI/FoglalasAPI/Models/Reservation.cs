using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoglalasAPI.Models
{
    public class Reservation
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }
        [Required]
        [ForeignKey("RestaurantFK")]
        public Restaurant Restaurant { get; set; }
        [Required]
        [ForeignKey("UserFK")]
        public User User { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime FinishedTime { get; set; }
        [Required]
        public bool Outdoor { get; set; }
        [Required]
        public bool SeperateRoom { get; set; }
    }
}
