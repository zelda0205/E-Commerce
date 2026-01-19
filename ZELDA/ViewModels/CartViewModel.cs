namespace ZELDA.ViewModels
{
    public class CartViewModel
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new();
        public decimal GrandTotal => Items.Sum(i => i.Total);
    }
}
