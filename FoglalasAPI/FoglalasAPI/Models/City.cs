using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoglalasAPI.Models
{
    [Table("Cities")]
    public class City
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
