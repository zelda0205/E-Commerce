using System.ComponentModel.DataAnnotations;

namespace ZELDA.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public string UserId { get; set; } = null!; // id e usersave esht string jo int.
        public virtual ApplicationUser? User { get; set; }

        [Display(Name ="Order Date")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Order Items")]
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}