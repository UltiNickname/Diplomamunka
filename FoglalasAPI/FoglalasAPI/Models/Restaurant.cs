using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoglalasAPI.Models
{
    [Table("Restaurants")]
    public class Restaurant
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [ForeignKey("City_Id")]
        public City City { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public bool Outdoor { get; set; }
        [Required]
        public bool SeperateRoom { get; set; }
        [Required]
        public bool FixedTables { get; set; }
    }
}
