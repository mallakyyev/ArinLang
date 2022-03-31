using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using ARINLAB.Services.Email;
using ARINLAB.Services.Settings;
using DAL.Models.Dto.EmailsModelDTO;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ARINLAB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Content-Manager")]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly ISettingsService _settings;
        private readonly ApplicationDbContext _dbContext;

        static private bool isSend;
        public EmailController(IEmailService emailService, ISettingsService settings, ApplicationDbContext dbContext)
        {
            _emailService = emailService;
            _settings = settings;
            _dbContext  = dbContext;
        }
        public IActionResult Index()
        {           
            return View();
        }

        public async Task<IActionResult> SendEmail(EmailsDTO emailModel)
        {
            List<string> emails = new List<string>();
            if(emailModel.SendToOrdinary )
                emails.AddRange(_dbContext.Users.Where(p => !string.IsNullOrEmpty(p.Email)).Select(p => p.Email));
            if (emailModel.SendToSubscribers)
                emails.AddRange(_dbContext.Subscribers.Where(p => !string.IsNullOrEmpty(p.Email)).Select(p => p.Email));
            emails = emails.Distinct().ToList();
             isSend = await _emailService.SendEmail(emailModel, emails);
             if(isSend)
                return RedirectToAction("ErrorView");
            ViewBag.Error = "Unknown Error, Please try again later!!";
            return View("Index");
             
        }

        public ActionResult ErrorView()
        {
            return View(isSend);
        }
    }
}