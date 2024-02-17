using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoglalasAPI.Models
{
    [PrimaryKey("Restauarnt", "Table")]
    public class RestaurantTables
    {
        [Required]
        [ForeignKey("RestaurantFK")]
        public int Restauarnt { get; set; }
        [Required]
        [ForeignKey("TableFK")]
        public int Table { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
