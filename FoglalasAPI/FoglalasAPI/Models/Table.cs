using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace FoglalasAPI.Models
{
    [Table("Tables")]
    [PrimaryKey("TableId", "RestaurantFK")]
    public class Table
    {
        [Key, Column(Order = 0)]
        public int TableId { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        [ForeignKey("RestaurantFK")]
        public Restaurant Restaurant { get; set; }
        [Required]
        public int Size { get; set; }
    }
}
