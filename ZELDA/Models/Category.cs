using System.ComponentModel.DataAnnotations;
using ZELDA.PersonalizedValidator;
namespace ZELDA.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [AllowedCategoryName(new string[] { "Clothes", "Accessories", "Bags" })]
        public string Name { get; set; } = null!;

        [MaxLength(255)]
        public string Description { get; set; } = null!;

        public virtual ICollection<Product>? Products { get; set; }
    }
}