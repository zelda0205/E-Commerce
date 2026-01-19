using System.ComponentModel.DataAnnotations;
using ZELDA.ViewModels;
namespace ZELDA.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Phone]
        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //public ICollection<CartViewModel> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
