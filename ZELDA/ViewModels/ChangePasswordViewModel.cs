using System.ComponentModel.DataAnnotations;

namespace ZELDA.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "The current password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "The current password")]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "The new password is required.")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "The new password ")]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and the old password do not match !")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
