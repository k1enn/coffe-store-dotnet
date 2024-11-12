    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    namespace BoysCoffe.Models
    {
        public class Category
        {
            [Key]
            public int CategoryId { get; set; }

            [Required]
            public string Name { get; set; }

            public virtual ICollection<Product> Products { get; set; }
        }

    }
