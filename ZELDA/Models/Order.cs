using System.ComponentModel.DataAnnotations;

namespace ZELDA.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        // Navigation Properties
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
