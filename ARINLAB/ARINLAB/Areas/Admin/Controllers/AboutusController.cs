using ARINLAB.Services;
using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Areas.Admin.Controllers
{
    [Area(Roles.Admin)]
    [Authorize(Roles = Roles.Admin)]
    public class AboutusController : Controller
    {
        private readonly AboutService _aboutService;
        public AboutusController(AboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public IActionResult Index()
        {
            return View(_aboutService.GetAboutus());
        }

        [HttpPost]
        public IActionResult Edit(DAL.Models.Aboutus aboutus)
        {
            var res = _aboutService.Edit(aboutus);
            return RedirectToAction("Index");
        }
    }
}
