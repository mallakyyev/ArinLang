using ARINLAB.Services.ImageService;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class LogoController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IImageService _imageService;
        public LogoController(ApplicationDbContext applicationDbContext, IImageService imageService)
        {
            _dbContext = applicationDbContext;
            _imageService = imageService;
        }
        public IActionResult Index()
        {
            LogoDto model = new LogoDto();
            var mainLogo = _dbContext.Logos.FirstOrDefault(p => p.Name.Contains("Main"));
            var secondaryLogo = _dbContext.Logos.FirstOrDefault(p => p.Name.Contains("Sec"));
            if(mainLogo!=null)
            {
                model.Id1 = mainLogo.Id;
                model.Image1 = mainLogo.Image;
                model.Name1 = mainLogo.Name;
                model.Link1 = mainLogo.Link;
            }
            if(secondaryLogo != null)
            {
                model.Id2 = secondaryLogo.Id;
                model.Image2 = secondaryLogo.Image;
                model.Name2 = secondaryLogo.Name;
                model.Link2 = secondaryLogo .Link;

            }
            ////var res = new List<Logo>(_dbContext.Logos);
            //if (res.Count > 0)
            //{
            //    if (res.Count > 1)
            //    {
            //        model.Id2 = res[1].Id;
            //        model.Image2 = res[1].Image;
            //        model.Name2 = res[1].Name;
            //        model.Link2 = res[1].Link;
            //    }
            //        model.Id1 = res[0].Id;
            //        model.Image1 = res[0].Image;
            //        model.Name1 = res[0].Name;
            //        model.Link1 = res[0].Link;               
            //}
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoUpdateAsync(LogoDto model)
        {
            string img1 = "";
            string img2 = "";
            if (model.ImageForm1!= null)
            {
                 img1 = await _imageService._UploadImage(model.ImageForm1, "Logo");
            }
            if(model.ImageForm2 != null)
            {
                 img2 = await _imageService._UploadImage(model.ImageForm2, "Logo");
            }

            var mainLogo =  _dbContext.Logos.FirstOrDefault(p => p.Name.Contains("Main"));
            var secondaryLogo = _dbContext.Logos.FirstOrDefault(p => p.Name.Contains("Sec"));
            if (mainLogo != null && secondaryLogo != null) {
                var mainL = await _dbContext.Logos.FindAsync(mainLogo.Id);
                var secL = await _dbContext.Logos.FindAsync(secondaryLogo.Id);
                if (img1 != "") {
                    mainL.Image = img1;
                }

                if(img2 != "")
                {
                    secL.Image = img2;
                }
                mainL.Link = model.Link1;
                secL.Link = model.Link2;
                _dbContext.Update(mainL);
                _dbContext.Update(secL);
                _dbContext.SaveChanges();
             }

            return RedirectToAction("Index");
        }

    }
}
