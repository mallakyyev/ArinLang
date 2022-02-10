using ARINLAB.Services;
using AutoMapper;
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
    [Area(Roles.Admin)]
    [Authorize(Roles = Roles.Admin)]
    public class WordClauseCategoryController : Controller
    {
        private readonly IWordClauseService _wordClauseServices;
        private readonly IMapper _mapper;
        private readonly ILanguageService _languageService;
        private readonly ApplicationDbContext _dbContext;
        public WordClauseCategoryController(IWordClauseService wordClause, IMapper mapper, ILanguageService languageService, ApplicationDbContext dbContext)
        {
            _wordClauseServices = wordClause;
            _mapper = mapper;
            _languageService = languageService;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var res = _wordClauseServices.GetAllWordClauseCategories();           
            return View(res);
        }

        public IActionResult Create()
        {
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            ViewBag.Categories = _wordClauseServices.GetAllWordClauseCategories();
            int count = _wordClauseServices.GetAllWordClauseCategories().Count;
            return View(new CreateWordClauseCategoryDto() { WordClauseCategoryTranslates = new List<DAL.Models.WordClauseCategoryTranslate>(count)});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWordClauseCategoryDto cat)
        {
            if (ModelState.IsValid)
            {
                await _wordClauseServices.CreateWordClauseCategory(cat);
                return RedirectToAction("Index");
            }
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            ViewBag.Categories = _wordClauseServices.GetAllWordClauseCategories();
            return View(cat);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            ViewBag.Languages = _languageService.GetAllPublishLanguage().OrderBy(o => o.DisplayOrder);
            WordClauseCategoryDto item = await _wordClauseServices.GetWordClauseCategoryByIdAsync(id);
            if (item != null)
            {
                var it = _wordClauseServices.GetAllWordClauseCategories();
                it.Remove(it.FirstOrDefault(p => p.Id == item.Id));
                ViewBag.Categories = it;
            }
            int count = _wordClauseServices.GetAllWordClauseCategories().Count;
            var res = _dbContext.WordClauseCategories.SingleOrDefault(p => p.Id == id);
            if (res == null)
                return RedirectToAction("Index");
            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(WordClauseCategory editCat)
        {
            try
            {
                foreach (var item in editCat.WordClauseCategoryTranslates)
                {
                    if (string.IsNullOrEmpty(item.CategoryName))
                        return RedirectToAction("Edit", new { id = editCat.Id });
                }
                _dbContext.WordClauseCategories.Update(editCat);
                await _dbContext.SaveChangesAsync();
            }catch(Exception e)
            {
                return RedirectToAction("Edit", new { id = editCat.Id });
            }
            return RedirectToAction("Index");
            
        }

    }
}
