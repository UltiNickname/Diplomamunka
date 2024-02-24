using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoglalasAPI.Models
{
    [Keyless]
    public class RestaurantTables
    {
        [Required]
        public int RestaurantId { get; set; }
        [Key, ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        [Required]
        public int TableId { get; set; }
        [Key, ForeignKey("TableId")]
        public virtual Table Table { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
