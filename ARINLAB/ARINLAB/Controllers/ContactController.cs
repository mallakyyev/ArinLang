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
        public ContactController(ContactService contact)
        {
            _cntController = contact;
        }
        public IActionResult Us()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Us(string name, string email, string subject)
        {
            if(string.IsNullOrEmpty(name) || 
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(subject)){
                ViewBag.Error = "0";
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
                ViewBag.Success = "1";
                return View();
            }
            ViewBag.Success = "0";
            return View();
        }
    }
}
