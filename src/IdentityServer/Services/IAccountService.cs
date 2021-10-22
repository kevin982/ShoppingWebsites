using IdentityMicroservice.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IdentityMicroservice.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordRequestModel model);
        Task<IdentityResult> ConfirmEmailAsync(string token, string userId);
        Task<IdentityResult> CreateUserAsync(SignUpRequestModel model);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordRequestModel model);
        Task SendEmailToResetPasswordAsync(EmailResetPasswordRequestModel model);
        Task<ApplicationUser> CustomFindUserAsync(string findBy, string value);
    }
}