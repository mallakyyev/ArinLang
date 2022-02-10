using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARINLAB.Services.Pages;
using Microsoft.AspNetCore.Mvc;

namespace TSTB.Web.Controllers
{
    public class PageController : Controller
    {
        private readonly IPagesService _pagesService;

        public PageController(IPagesService pagesService)
        {
            _pagesService = pagesService;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _pagesService.GetPageById(id);
            if(result != null)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("404");
            }
           
        }
    }
}
