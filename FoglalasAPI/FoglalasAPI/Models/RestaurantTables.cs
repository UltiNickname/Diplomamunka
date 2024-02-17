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
        public Restaurant RestauarntId { get; set; }
        [Required]
        [ForeignKey("TableFK")]
        public Table TableId { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
