namespace BoysCoffe.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Price at the time of adding to cart
        public decimal TotalPrice { get => Quantity * Price; }
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }

}

