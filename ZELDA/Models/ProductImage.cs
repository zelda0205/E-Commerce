using System.ComponentModel.DataAnnotations;

namespace ZELDA.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageID { get; set; }
        public string ImageUrl { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
