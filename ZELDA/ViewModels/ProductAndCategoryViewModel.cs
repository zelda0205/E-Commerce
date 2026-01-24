using ZELDA.Models;

namespace ZELDA.ViewModels
{
    public class ProductAndCategoryViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public string? SelectedCategory { get; set; }
    }
}