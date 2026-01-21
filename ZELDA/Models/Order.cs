using System.ComponentModel.DataAnnotations;

namespace ZELDA.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public int UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}
