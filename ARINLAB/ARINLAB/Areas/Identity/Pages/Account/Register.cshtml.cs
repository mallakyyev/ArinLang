using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ARINLAB.Services.Email;
using DAL.Data;
using DAL.Enums;
using DAL.Models;
using DAL.Models.Configs;
using DAL.Models.Dto.EmailsModelDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace ARINLAB.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailService _emailService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, ApplicationDbContext dbContext, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _dbContext = dbContext;
            _emailService = emailService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public class SelectListItemExtended : SelectListItem
        {
            public string Id { get; set; }
        }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]            
            public int CountryId { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            public string Name { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            public string FamilyName { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Required]
            public Gender Gender { get; set; }

            public string Occupation { get; set; }

            public string MoreInfor { get; set; }

           
        }
       
            public List<SelectListItemExtended> l = new List<SelectListItemExtended>();
            public bool emailInUse { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
          
            List<Country> countr = new List<Country>(_dbContext.Countries);
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            foreach (Country c in countr)
            {
                if (culture == "ar")
                    l.Add(new SelectListItemExtended { Text = c.Name_ar, Value = c.Id + "", Id = $"{c.CountryCode}" });
                else
                    l.Add(new SelectListItemExtended { Text = c.Name, Value = c.Id + "", Id = $"{c.CountryCode}" });
            }

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                emailInUse = false;
                var existing_user = await _userManager.FindByEmailAsync(Input.Email);
                if (existing_user != null)
                {
                    emailInUse = true;
                    ModelState.AddModelError("Email", "This email already in use");
                    string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                    List<Country> countr1 = new List<Country>(_dbContext.Countries);
                    foreach (Country c in countr1)
                    {
                        if(culture =="ar")
                            l.Add(new SelectListItemExtended { Text = c.Name_ar, Value = c.Id + "", Id = $"{c.CountryCode}" });
                        else
                            l.Add(new SelectListItemExtended { Text = c.Name, Value = c.Id + "", Id = $"{c.CountryCode}" });
                    }
                    // If we got this far, something failed, redisplay form
                    return Page();
                }
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email,  FirstName = Input.Name, 
                                FamilyName = Input.FamilyName, CountryId = Input.CountryId, PhoneNumber = Input.PhoneNumber, 
                                Gender = Input.Gender, IsApproved = false };

                
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Registered);
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);
                    int Ecode = 0;
                    foreach(char c in Input.Email)
                    {
                        Ecode += char.ToUpper(c)*7;
                    }
                    Ecode = 100000 + (Ecode % 89999);
                    SingleEmailDTO mailMessage = new SingleEmailDTO();
                    mailMessage.AdminEmail = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmail")).Value;
                    mailMessage.EmailTo = Input.Email;
                    mailMessage.Password = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmailPassword")).Value;
                    mailMessage.Subject = "Verify code from ARINLANG";
                    mailMessage.Message = "Your Email Confirmation for ARINLANG Your Code is " + Ecode;
                    var res =  _emailService.SendSingleEmail(mailMessage);
                    
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            List<Country> countr = new List<Country>(_dbContext.Countries);
            foreach (Country c in countr)
            {
                l.Add(new SelectListItemExtended { Text = c.Name, Value = c.Id + "", Id = $"{c.CountryCode}" });
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
