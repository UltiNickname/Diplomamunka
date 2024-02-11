using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoglalasAPI.Models
{
    [Table("Tables")]
    [PrimaryKey("Id")]
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        [Required]
        [ForeignKey("Restaurant_Id")]
        public Restaurant RestaurantId { get; set; }
        [Required]
        public int Size { get; set; }
    }
}
