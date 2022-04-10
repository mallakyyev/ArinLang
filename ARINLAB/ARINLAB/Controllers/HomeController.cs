using ARINLAB.Models;
using ARINLAB.Services;
using ARINLAB.Services.Email;
using ARINLAB.Services.News;
using ARINLAB.Services.SessionService;
using ARINLAB.Services.Settings;
using ARINLAB.Services.Statistic;
using ARINLAB.Services.Subscribe;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto;
using DAL.Models.Dto.EmailsModelDTO;
using DAL.Models.Dto.NewsModelDTO;
using DAL.Models.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARINLAB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStatisticsService _statService;
        private readonly UserDictionary _userDict;
        private readonly IDictionaryService _dictService;
        private readonly IWordServices _wordServices;
        private readonly INewsService _newsService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ISubscribeService _subscriberService;
        private readonly ISettingsService _settings;
        private readonly IEmailService _emailService;
        private readonly BagService _bagService;
        private readonly ApplicationDbContext _dbContext;
        private readonly ReCaptcha _captcha;
        public HomeController(ILogger<HomeController> logger, IStatisticsService statisticsService, 
                              UserDictionary userDictionary, IDictionaryService dictionaryService,
                              IWordServices wordServices, INewsService newsService, IStringLocalizer<SharedResource> localizer,
                              ISettingsService settingsService, ISubscribeService subscribeService, IEmailService emailService,
                              ApplicationDbContext applicationDb, BagService bagService,  ReCaptcha captcha)
        {
            _logger = logger;
            _statService = statisticsService;
            _userDict = userDictionary;
            _dictService = dictionaryService;
            _wordServices = wordServices;
            _newsService = newsService;
            _localizer = localizer;
            _settings = settingsService;
            _subscriberService = subscribeService;
            _emailService = emailService;
            _dbContext = applicationDb;
            _bagService = bagService;
            _captcha = captcha;
        }

        public IActionResult Index()
        {
            var dicts = _dictService.GetAllDictionaries();
            ViewBag.Dictionaries = dicts;
            HomeViewModel model = new HomeViewModel() { StatistiCards = _statService.GetStatisticsCard() };
            ViewBag.News = (List<NewsDTO>)(_newsService.GetFourPublishNews().ToList());

            return View(model);
        }

        public IActionResult SetDictionary(int dictId)
        {
            _userDict.SetDictionary(dictId);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl, string value)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect($"{returnUrl}{value}");
        }

        [HttpGet("Unsubscribe")]
        public ActionResult Unsubscribe()
        {
            return View();
        }

        
        public async Task<IActionResult> Unsubscribed(string email)
        {
            string id = _subscriberService.GetEmail(email);
            if (!string.IsNullOrEmpty(id))
            {
                await _subscriberService.DeleteSubscriber(id);
                ViewBag.Email = _localizer["You have succeffully unsubscribed"];
                return View();
            }
            ViewBag.Email = email + _localizer[" email not in subcribers list!!"];
            return View();

        }
        public IActionResult UnsubLink(string id)
        {
            _subscriberService.DeleteSubscriber(id);
            ViewBag.UnSub = _localizer["You have succeffully unsubscribed"];
            return View();
        }

        public async Task<string> Subscribe([FromQuery]string email)
        {
            Subscribers sub = new Subscribers();
            sub.Email = email;
            sub.Id = Guid.NewGuid().ToString();
            bool isSub = await _subscriberService.AddSubscriber(sub);
            if (isSub)
            {               
                SingleEmailDTO mailMessage = new SingleEmailDTO();
                mailMessage.AdminEmail = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmail")).Value;
                mailMessage.EmailTo = email;
                mailMessage.Password = _dbContext.Settings.FirstOrDefault(p => p.Name.Contains("AdminEmailPassword")).Value;
                mailMessage.Subject = "Thank your for subscription to ARINLANG";
                mailMessage.Message = "Stay up to date with new words, phrases and much more. ";
                var res = _emailService.SendSingleEmailAsync(mailMessage);
                return email + " " + _localizer["Email successfully subscribed"];
            }
            else
            {
                return  "";
            }
           
        }
        public IActionResult Setsettings(int dictId, string lang, string value, string returnUrl)
        {
            _userDict.SetDictionary(dictId);
            Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
           );
            return LocalRedirect($"{returnUrl}{value}");            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBags(string email, string problem, string link)
        {
            int last = link.IndexOf('&');
            if(last != -1)
                link = link.Substring(0, last);
            if (!Request.Form.ContainsKey("g-recaptcha-response"))
            {
                ModelState.AddModelError(string.Empty, "reCAPTCHA error.");
                return LocalRedirect(link + "&bag=bagerror");
            }
            var captcha = Request.Form["g-recaptcha-response"].ToString();
            if (!await _captcha.IsValid(captcha))
            {
                ModelState.AddModelError(string.Empty, "reCAPTCHA error.");
                return LocalRedirect(link + "&bag=bagerror");
            }

            CreateBagDto bag = new CreateBagDto()
            {
                Email = email,
                Problem = problem,
                Link = link
            };
            _bagService.CreateBag(bag);

            if (link.Contains("WordClauses"))
            {
                string [] path = link.Split('/');
                int wid = int.Parse(path[3]);
                return RedirectToAction("Details", "WordClauses", new { id = wid, bag="success" });
            }

            
            if(last == -1)
                return LocalRedirect(link + "&bag=success");
            link = link.Substring(0, last );
            
            return LocalRedirect(link + "&bag=success");
        }
    }
}
