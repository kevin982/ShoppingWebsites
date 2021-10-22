using System.ComponentModel.DataAnnotations;

namespace IdentityMicroservice.Models
{
    public class SignUpRequestModel
    {
        [Required(ErrorMessage = "You must enter your user name"), Display(Description = "Enter your user name"), MaxLength(20), MinLength(4)]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must enter your email"), Display(Description = "Enter your email"), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must enter a password"), Display(Description = "Enter the password"), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must confirm your password"), Display(Description = "Confirm your password"), DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        
        [Display(Description = "Select in you are shopping owner")]
        public bool IsOwner { get; set; }

        [Display(Description = "Select in you are shopping costumer")]
        public bool IsCostumer { get; set; }
    }
}
