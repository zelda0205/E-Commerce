using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace ZELDA.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsBlocked { get; set; } = false;
    }
}
