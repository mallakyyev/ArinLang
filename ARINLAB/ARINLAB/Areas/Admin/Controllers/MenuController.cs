using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ARINLAB.Services.Menu;
using ARINLAB.Services.Pages;
using ARINLAB.Services;
using DAL.Data;
using DAL.Models.Dto.MenuModelDTO;

namespace ARINLAB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class MenuController : Controller
    {
      

        private readonly IMenuService _menuService;
        private readonly IPagesService _pagesService;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public MenuController(IMenuService menuService, IPagesService pagesService, ILanguageService languageService,IMapper mapper)
        {
            _menuService = menuService;
            _pagesService = pagesService;
            _languageService = languageService;
            _mapper = mapper;
        }
        // GET: Admin/Menu
        public IActionResult Index()
        {
            return View();
        }


        // GET: Admin/Menu/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View();
        }

        // Post: Admin/Menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMenuDTO menu)
        {
            if (ModelState.IsValid)
            {
                await _menuService.CreateMenu(menu);
              
                return RedirectToAction("Index");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View(menu);
        }

        [HttpGet]
        // GET: Admin/Menu/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var menu = await _menuService.GetMenuForEditById(id);
            if (menu == null)
            {
                return NotFound();
            }

            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            //ViewBag.CategorySelection = new SelectList(_categoryService.GetAllCategory(), "Id", "Name", category.ParentCategoryId);

            return View(menu);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditMenuDTO editMenuDTO)
        {
            if (ModelState.IsValid)
            {             
                await _menuService.EditMenu(editMenuDTO);
                return RedirectToAction("Index");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);

            return View(editMenuDTO);
        }

        // GET: Admin/Project/Delete/5
        public IActionResult Delete(int? id)
        {
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View();
        }

    }
}