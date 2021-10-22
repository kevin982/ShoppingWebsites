using System.ComponentModel.DataAnnotations;

namespace IdentityMicroservice.Models
{
    public class ResetPasswordRequestModel
    {
        public string Id { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must enter the new password"), Display(Name = "Please enter your new password"), DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must enter the new password"), Display(Name = "Please confirm your new password"), DataType(DataType.Password), Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
