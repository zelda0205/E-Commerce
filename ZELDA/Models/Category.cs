using System.ComponentModel.DataAnnotations;

namespace ZELDA.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string Name { get; set; } = null!;

        [MaxLength(255)]
        public string Description { get; set; } = null!;

        public virtual ICollection<Product>? Products { get; set; }
    }
}