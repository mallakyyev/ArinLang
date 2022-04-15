﻿using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using DAL.Models;
using System.ComponentModel.DataAnnotations;
using DAL.Models.Dto.EmailsModelDTO;
using DAL.Data;
using ARINLAB.Services.Email;
using System.Linq;

namespace ARINLAB.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _sender;
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailService _emailService;


        public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender sender,
                        ApplicationDbContext dbContext, IEmailService emailService)
        {
            _userManager = userManager;
            _sender = sender;
            _dbContext = dbContext;
            _emailService = emailService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            public string Email { get; set; }

            [Required]
            public int ECode { get; set; }

        }
        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null, string resend = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            if (!string.IsNullOrEmpty(resend))
            {
                StatusMessage = "2";
                int Ecode = 0;
                foreach (char c in email)
                {
                    Ecode += char.ToUpper(c) * 7;
                }
                Ecode = 100000 + (Ecode % 89999);
                SingleEmailDTO mailMessage = new SingleEmailDTO();
                mailMessage.AdminEmail = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmail")).Value;
                mailMessage.EmailTo = email;
                mailMessage.Password = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmailPassword")).Value;
                mailMessage.Subject = "Verify code from ARINLANG";
                mailMessage.Message = "Your Email Confirmation for ARINLANG Your Code is " + Ecode;
                var res = await _emailService.SendSingleEmailAsync(mailMessage);
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }
            
            Email = email;
            // Once you add a real email sender, you should remove this code that lets you confirm the account
            DisplayConfirmAccountLink = false;
            if (DisplayConfirmAccountLink)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
            }

            return Page();
        }       

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                StatusMessage = "0";
                return Page();
            }
            int Ecode = 0;
            foreach (char c in Input.Email)
            {
                Ecode += char.ToUpper(c) * 7;
            }
            Ecode = 100000 + (Ecode % 89999);
            if(Ecode == Input.ECode)
            {
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;
                await _userManager.UpdateAsync(user);
            }
            StatusMessage = Ecode==Input.ECode ? "1" : "0";
            
            return RedirectToPage("./RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
        }
    }
}
