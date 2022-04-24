using ARINLAB.Services.ImageService;
using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Areas.Admin.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Area("Admin")]
    public class ClearImageCashController : Controller
    {
        private readonly IImageService _imageService;
        public ClearImageCashController(IImageService imageService)
        {
            _imageService = imageService;
        }
        public IActionResult Confirm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Success(string text)
        {
            ViewBag.Success = text;
            return View();
        }

        [HttpPost]
        public IActionResult Clear()
        {
            string t = "0";
            bool result = _imageService.ClearImageCash();
            
            if (result)
                t = "1";
           
            return RedirectToAction("Success", new { text = t});
        }

        
    }
}
