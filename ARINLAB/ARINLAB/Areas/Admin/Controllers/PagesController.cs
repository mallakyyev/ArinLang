using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ARINLAB.Services.Menu;
using ARINLAB.Services;
using ARINLAB.Services.Pages;
using DAL.Models.Dto.MenuModelDTO;

namespace ARINLAB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Content-Manager")]
    public class PagesController : Controller
    {
        
        private readonly IMenuService _menuService;
        private readonly ILanguageService _languageService;
        private readonly IPagesService _pagesService;
        

        public PagesController(IMenuService menuService, ILanguageService languageService, IPagesService pagesService)
        {
            _pagesService = pagesService;
            _languageService = languageService;
            _menuService = menuService;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePagesDTO value)
        {
            if (ModelState.IsValid)
            {
                await _pagesService.CreatePages(value);
                ViewBag.ErrorMessage = "";
                return RedirectToAction("Index");              
            }
           
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            ViewBag.ErrorMessage = "Menu have not been selected or all menus has a page";
            return View(value);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _pagesService.GetPagesForEditById(id);

            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPageDTO value)
        {
            if (ModelState.IsValid)
            {
                await _pagesService.EditPages(value);
                return RedirectToAction("Index");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);

            return View(value);
        }
    }
}