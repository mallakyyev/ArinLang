using ARINLAB.Models;
using ARINLAB.Services;
using DAL.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactService _cntController;
        private readonly ReCaptcha _captcha;
        public ContactController(ContactService contact, ReCaptcha captcha)
        {
            _cntController = contact;
            _captcha = captcha;
        }
        public IActionResult Us()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UsAsync(string name, string email, string subject)
        {

            if(string.IsNullOrEmpty(name) || 
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(subject)){
                ViewBag.Result = "0";
                return View();
            }
            if (!Request.Form.ContainsKey("g-recaptcha-response"))
            {
                ViewBag.Result = "0";
                return View();
            }
            var captcha = Request.Form["g-recaptcha-response"].ToString();
            if (!await _captcha.IsValid(captcha))
            {
                ViewBag.Result = "0";
                return View();
            }
            CreateContactDto model = new()
            {
                Name = name,
                Email = email,
                Subject = subject
            };
            var res = _cntController.CreateContact(model);
            if (res)
            {
                ViewBag.Result = "1";
                return View();
            }
            ViewBag.Result = "0";
            return View();
        }
    }
}
