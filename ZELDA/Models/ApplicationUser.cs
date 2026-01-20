using Microsoft.AspNetCore.Identity;
namespace ZELDA.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
        public bool IsBlocked { get; set; } = false;
    }
}
