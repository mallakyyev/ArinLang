using System;
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
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _dbContext;

        public ResendEmailConfirmationModel(UserManager<ApplicationUser> userManager, IEmailService emailSender, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _emailService = emailSender;
            _dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            int Ecode = 0;
            foreach (char c in Input.Email)
            {
                Ecode += char.ToUpper(c) * 7;
            }
            Ecode = 100000 + (Ecode % 89999);
            SingleEmailDTO mailMessage = new SingleEmailDTO();
            mailMessage.AdminEmail = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmail")).Value;
            mailMessage.EmailTo = Input.Email;
            mailMessage.Password = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmailPassword")).Value;
            mailMessage.Subject = "Verify code from ARINLANG";
            mailMessage.Message = "Your Email Confirmation for ARINLANG Your Code is " + Ecode;
            var res = _emailService.SendSingleEmailAsync(mailMessage);

            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = "#" });
        }
    }
}
