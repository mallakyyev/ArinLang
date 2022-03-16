using ARINLAB.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly AboutService _service;
        public AboutUsController(AboutService service)
        {
            _service = service; 
        }
        public IActionResult About()
        {
            return View(_service.GetAboutus());
        }
    }
}
