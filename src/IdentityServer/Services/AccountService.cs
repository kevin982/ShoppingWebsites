using IdentityMicroservice.Mappers;
using IdentityMicroservice.Models;
 
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
 
using System.Threading.Tasks;
 
namespace IdentityMicroservice.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager = null;
        private readonly IConfiguration Configuration = null;
        private readonly IAccountMapper _accountMapper = null;

        public AccountService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IAccountMapper accountMapper)
        {
            _userManager = userManager;
            Configuration = configuration;
            _accountMapper = accountMapper;
        }

        public async Task<ApplicationUser> CustomFindUserAsync(string findBy, string value)
        {
            if (string.IsNullOrEmpty(findBy) || string.IsNullOrEmpty(value)) throw new Exception("The findby and value can not be null or empty");

            findBy = findBy.ToLower();

            if (findBy != "id" && findBy != "email") throw new Exception("The find by can only be id or email");

            ApplicationUser user = (findBy == "id") ? await _userManager.FindByIdAsync(value) : await _userManager.FindByEmailAsync(value);

            if (user is null) throw new Exception("The user could no be found");

            if (user.UsedExternalProvider) throw new Exception("Could not change password since you used an external identity provider!");

            return user;
        }
 
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordRequestModel model)
        {
            try
            {
                if (model is null) throw new Exception("The model to change the password can not be null");

                ApplicationUser user = await CustomFindUserAsync("id", model.UserId);

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (!result.Succeeded) throw new Exception(result.Errors.ToList()[0].Description);

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IdentityResult> ConfirmEmailAsync(string token, string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId)) throw new Exception("The userId and token must not be null or empty");

                ApplicationUser user = await CustomFindUserAsync("id", userId);

                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (!result.Succeeded) throw new Exception(result.Errors.ToList()[0].Description);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpRequestModel model)
        {
            try
            {
                if (model is null) throw new Exception("The model to create the user can not be null");

                var user = _accountMapper.MapSignUpRequestModelToDomain(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded) throw new Exception(result.Errors.ToList()[0].Description);

                if (model.Email == Configuration["AdminEmail"])
                {
                    var resultRoleAdmin = await _userManager.AddToRoleAsync(user, "admin");
                    if(!resultRoleAdmin.Succeeded) throw new Exception(resultRoleAdmin.Errors.ToList()[0].Description);
                }
                else
                {
                    string role = (model.IsOwner)?"owner":"costumer";
                                        
                    var resultRoleUser = await _userManager.AddToRoleAsync(user, role);

                    if (!resultRoleUser.Succeeded) throw new Exception(resultRoleUser.Errors.ToList()[0].Description);
                }

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                if (string.IsNullOrEmpty(token)) throw new Exception("The token could not be generated!");

                await SendEmailConfirmationAsync(token, user.Email, user.Id, user.UserName);

                return result;

            }
            catch (Exception)
            {
                if (model is null) throw;

                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is null) throw;
                
                await _userManager.RemoveFromRolesAsync(user, new List<string>{ "owner", "admin", "costumer"});
                await _userManager.DeleteAsync(user);
                
                throw;
            }
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordRequestModel model)
        {
            try
            {
                if (model is null) throw new Exception("The model to reset the password can not be null");

                var user = await CustomFindUserAsync("id", model.Id);

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

                if (!result.Succeeded) throw new Exception(result.Errors.ToList()[0].Description);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendEmailToResetPasswordAsync(EmailResetPasswordRequestModel model)
        {
            try
            {
                if (model is null) throw new Exception("The model to send the email to reset the password can not be null");

                var user = await CustomFindUserAsync("email", model.UserEmail);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                string htmlparent = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();

                string htmlPath = htmlparent += "\\IdentityServer\\Mails\\ResetPassword.html";

                string subject = "Reset Password";
                List<Attachment> attachments = new();
                List<string> mails = new() { model.UserEmail };
                List<(string, string)> values = new() { ("Link", string.Format(Configuration.GetValue<string>("AppsUrls:Self") + $"/Account/ResetPassword?id={user.Id}&token={token}")), ("UserName", user.UserName) };
                await SendEmailAsync(subject, htmlPath, mails, attachments, values);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task SendEmailConfirmationAsync(string token, string email, string userId, string name)
        {
            try
            {

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(name)) throw new Exception("The token, email, user id and name can not be null or empty");

                string htmlparent = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();

                string htmlFile = htmlparent += "\\IdentityServer\\Mails\\ConfirmEmail.html";

                string subject = "Email Confirmation";
                List<Attachment> attachments = new();
                List<string> mails = new() { email };
                List<(string, string)> values = new() { ("Link", string.Format(Configuration.GetValue<string>("AppsUrls:Self") + $"/Account/ConfirmEmail?id={userId}&token={token}")), ("UserName", name) };
                await SendEmailAsync(subject, htmlFile, mails, attachments, values);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private async Task SendEmailAsync(string subject, string htmlPath, List<string> mails, List<Attachment> attachments, List<(string, string)> values)
        {

            try
            {
                string html = await File.ReadAllTextAsync(htmlPath);

                MailMessage mail = new()
                {
                    Subject = subject,
                    Body = html,
                    From = new MailAddress(Configuration.GetValue<string>("SMTPConfig:EmailSender"), Configuration.GetValue<string>("SMTPConfig:DisplayName")),
                    IsBodyHtml = Configuration.GetValue<bool>("SMTPConfig:IsBodyHTML")
                };

                foreach (var emailAddress in mails) mail.To.Add(emailAddress);
                foreach (var attchment in attachments) mail.Attachments.Add(attchment);
                foreach (var value in values) mail.Body = mail.Body.Replace($"{{{{{value.Item1}}}}}", value.Item2);

                SmtpClient smtpClient = new()
                {
                    Host = Configuration.GetValue<string>("SMTPConfig:Host"),
                    Port = Configuration.GetValue<int>("SMTPConfig:Port"),
                    EnableSsl = Configuration.GetValue<bool>("SMTPConfig:EnableSSL"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("SMTPConfig:EmailSender"), Configuration.GetValue<string>("SMTPConfig:Password"))
                };

                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
 
    }
}
