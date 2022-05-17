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
    public class BagController : Controller
    {

        private readonly BagService _bagService;
        public BagController(BagService bagService)
        {
            _bagService = bagService;
        }
        public IActionResult Index(int id)
        {
            _bagService.Readed(id);
            return View();
        }
    }
}
