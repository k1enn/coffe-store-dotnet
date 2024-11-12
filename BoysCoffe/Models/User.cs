using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BoysCoffe.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }
        public string Role { get; set; } // "Admin" or "Customer"

        public virtual Cart Cart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

}

