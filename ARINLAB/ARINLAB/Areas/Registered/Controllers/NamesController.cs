﻿using ARINLAB.Models;
using ARINLAB.Services;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto.NamesDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ARINLAB.Areas.Registered.Controllers
{
    [Area(Roles.Registered)]
    [Authorize(Roles = Roles.Registered)]
    public class NamesController : Controller
    {
        private readonly INamesService _nameService;
        private readonly IDictionaryService _dictService;
        public NamesController(INamesService namesService, IDictionaryService dictionaryService)
        {
            _nameService = namesService;
            _dictService = dictionaryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {           
            ViewBag.Dictionaries = _dictService.GetAllDictionaries().Data;            
            return View(new CreateNamesDto());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateAsync(CreateNamesDto model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                model.IsApproved = false;
                var res = await _nameService.CreateNameAsync(model);
                if (res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }          
            ViewBag.Dictionaries = _dictService.GetAllDictionaries().Data;            
            return View("Create");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var res = await _nameService.GetNameByIdAsync(id);
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (res.UserId != userId)
                return RedirectToAction("Index");
            ViewBag.Dictionaries = _dictService.GetAllDictionaries().Data;
            if (res != null)
                return View(res);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditAsync(NamesDto model)
        {
            try
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (ModelState.IsValid)
                {
                    if (model.UserId != userId)
                        return RedirectToAction("Index");
                    model.NameImages = null;
                    var res = await _nameService.EditNameAsync(model);
                    if (res.IsSuccess)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }catch(Exception e)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> EditImageAsync(int id)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var clause = await _nameService.GetNameByIdAsync(id);
            if (clause == null)
                return RedirectToAction("Index");
            if (clause.UserId != userId)
                return RedirectToAction("Index");
           
            ViewBag.Dictionaries = _dictService.GetAllDictionaries().Data;
            ViewBag.Model = clause;
            var voices = _nameService.GetAllNamesImagesByNameIdAsync(id);
            return View(voices);
        }

        [HttpGet("/Registered/[controller]/AddImage/{id}")]
        public async Task<IActionResult> AddImageAsync(int id)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var res = await _nameService.GetNameByIdAsync(id);
            if (res == null)
                return RedirectToAction("EditImage", new { id = id });

            if (res.UserId != userId)
                return RedirectToAction("Index");

            NamesImagesViewModel model = new();
            model.Id = id;
            model.ArabName= res.ArabName;
            model.OtherName= res.OtherName;
            model.DictName = res.DictionaryName;
            CreateNameImagesDto file = new();
            ViewBag.Model = model;
            return View(file);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddImage(CreateNameImagesDto model)
        {

            model.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;            
            var res = await _nameService.CreateImageforNameAsync(model);
            if (res.IsSuccess)
            {
                return RedirectToAction("Edit", new { id = model.NamesId });
            }
            return RedirectToAction("Index");
        }

    }
}
