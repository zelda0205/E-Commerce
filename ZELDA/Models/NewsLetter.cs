using System.ComponentModel.DataAnnotations;

namespace ZELDA.Models
{
    public class NewsLetter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Subscribed At")]
        public DateTime SubscribedAt { get; set; } = DateTime.Now;
    }
}