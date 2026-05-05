using System.ComponentModel.DataAnnotations;
using ZELDA.Models;

namespace ZELDA.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        [Display(Name = "Emri")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Mbiemri")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datelindja")]
        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; } 

        public List<Order> OrderHistory { get; set; } = new List<Order>();
    }
}
