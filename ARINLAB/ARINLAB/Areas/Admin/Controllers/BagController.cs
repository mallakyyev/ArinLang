using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Areas.Admin.Controllers
{
    public class BagController : Controller
    {
        [Area(Roles.Admin)]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
