using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoglalasAPI.Models
{
    [PrimaryKey("RestauarntId", "TableId")]
    public class RestaurantTables
    {
        [Required]
        [ForeignKey("RestaurantFK")]
        public Restaurant Restauarnt { get; set; }
        [Required]
        [ForeignKey("TableFK")]
        public Table Table { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
