using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoysCoffe.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Sản phầm cần có hình ảnh")]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("Category")]
        public virtual Category Category { get; set; }
        // Add these navigation properties
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

}
