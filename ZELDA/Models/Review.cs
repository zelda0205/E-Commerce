using System.ComponentModel.DataAnnotations;

namespace ZELDA.Models
{
    public class Review
    {
        public int ReviewID { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
