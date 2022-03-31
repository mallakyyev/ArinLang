using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ARINLAB.Services.Email;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto.EmailsModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace ARINLAB.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailService _emailService;
        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender,
                                   ApplicationDbContext dbContext, IEmailService emailService)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _dbContext = dbContext;
            _emailService = emailService;
         }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);
                SingleEmailDTO mailMessage = new SingleEmailDTO();
                mailMessage.AdminEmail = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmail")).Value;
                mailMessage.EmailTo = Input.Email;
                mailMessage.Password = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmailPassword")).Value;
                mailMessage.Subject = "Reset Password from ARINLANG account";
                mailMessage.Message = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                var res = _emailService.SendSingleEmailAsync(mailMessage);                

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
