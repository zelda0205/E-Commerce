using System.ComponentModel.DataAnnotations;

namespace ZELDA.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(255)]
        public string Description { get; set; } = null!;

        public ICollection<Product>? Products { get; set; }
    }
}
