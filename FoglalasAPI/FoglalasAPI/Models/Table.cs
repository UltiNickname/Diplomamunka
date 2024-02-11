using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoglalasAPI.Models
{
    [Table("Tables")]
    [PrimaryKey("TableId")]
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TableId { get; set; }
        [Key]
        [Required]
        [ForeignKey("RestaurantFK")]
        public Restaurant Restaurant { get; set; }
        [Required]
        public int Size { get; set; }
    }
}
