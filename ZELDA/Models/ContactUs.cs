using System.ComponentModel.DataAnnotations;

namespace ZELDA.Models
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Your name is required")]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Your email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Your message is required")]
        [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
        public string Message { get; set; } = null!;

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
