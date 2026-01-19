namespace ZELDA.ViewModels
{
    public class CartItemViewModel
    {
        public int CartItemID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } 
        public string ImageUrl { get; set; } // ← from ProductImage
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}
