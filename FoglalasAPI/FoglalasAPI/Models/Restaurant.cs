using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoglalasAPI.Models
{
    [Table("Restaurants")]
    public class Restaurant
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestaurantId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [ForeignKey("CityFK")]
        public City City { get; set; }
        [Required]
        public bool Outdoor { get; set; }
        [Required]
        public bool SeperateRoom { get; set; }
        [Required]
        public bool FixedTables { get; set; }
        [Required]
        public List<Table> Tables { get; set; }
    }
}
