using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoglalasAPI.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
