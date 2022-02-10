using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using DAL.Data;
using ARINLAB.Services.News;
using ARINLAB.Services;
using DAL.Models.Dto.MenuModelDTO;
using DAL.Models.Dto.NewsModelDTO;

namespace ARINLAB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ILanguageService _languageService;

        public NewsController(INewsService newsService, ILanguageService langService)
        {
            _newsService = newsService;
            _languageService = langService;

        }
        // GET: Admin/News
        public IActionResult Index()
        {
            return View();
        }


        // GET: Admin/News/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View();
        }

        // Post: Admin/News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewsDTO news)
        {
            if (ModelState.IsValid)
            {
                await _newsService.CreateNews(news);
                return RedirectToAction("Index");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View(news);
        }

        [HttpGet]
        // GET: Admin/News/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var dept = await _newsService.GetNewsForEditById(id);

            if (dept == null)
            {
                return NotFound();

            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View(dept);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditNewsDTO pr)
        {
            if (ModelState.IsValid)
            {
                await _newsService.EditNews(pr);
                return RedirectToAction("Index");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);

            return View(pr);
        }

        // GET: Admin/News/Delete/5
        public IActionResult Delete(int? id)
        {
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            return View();
        }

    }
}