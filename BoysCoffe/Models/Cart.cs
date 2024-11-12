using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoysCoffe.Models
{
    public class Cart
    {
        [Key, ForeignKey("User")]
        public int CartId { get; set; }
        public virtual User User { get; set; }

        public int CustomerId { get; set; } // Optional if tied to a logged-in customer
        public virtual ICollection<CartItem> CartItems { get; set; }
    }

}

