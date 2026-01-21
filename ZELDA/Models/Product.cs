using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZELDA.CustomValidators;
using ZELDA.ViewModels;

namespace ZELDA.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        public string? Image { get; set; }

        [NotMapped]
        [ImageFileValidation]
        [Required(ErrorMessage = "Image file is required")]
        public virtual IFormFile? ImageFile { get; set; }

        [Display(Name ="Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}