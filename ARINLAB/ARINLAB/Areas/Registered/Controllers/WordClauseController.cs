﻿using ARINLAB.Models;
using ARINLAB.Services;
using AutoMapper;
using DAL.Data;
using DAL.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ARINLAB.Areas.Registered.Controllers
{
    [Area(Roles.Registered)]
    [Authorize(Roles = Roles.Registered)]
    public class WordClauseController : Controller
    {
        private readonly IWordClauseService _wordClauseService;
        private readonly IDictionaryService _dictService;
       
        private readonly IMapper _mapper;


        public WordClauseController(IWordClauseService wordClauseService, IMapper mapper, 
                                    IDictionaryService dictionaryService)
        {
            _wordClauseService = wordClauseService;
            _mapper = mapper;
            _dictService = dictionaryService;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Dictionaries = _dictService.GetAllDictionaries(false).Data;
            ViewBag.Categories = _wordClauseService.GetAllWordClauseCategories();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateAsync(CreateWordClauseDto model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;              
                var res = await _wordClauseService.CreateWordClause(model);
                if (res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }               
            }
            ViewBag.Data = model;
            ViewBag.Dictionaries = _dictService.GetAllDictionaries(false).Data;
            ViewBag.Categories = _wordClauseService.GetAllWordClauseCategories();
            return View("Create");
        }

        public async Task<IActionResult> Edit(int id)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var res = await _wordClauseService.GetWordClauseByIdAsync(id);
            if (res.UserId != userId)
                return RedirectToAction("Index");

                
            ViewBag.Dictionaries = _dictService.GetAllDictionaries(false).Data;
            ViewBag.Categories = _wordClauseService.GetAllWordClauseCategories();
            return View(res);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(EditWordClauseDto model)
        {
            if (ModelState.IsValid)
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != model.UserId)
                    return RedirectToAction("Index");                
                
                var res = await _wordClauseService.EditWordClause(model);               
               if (res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }                
            }
            return View();
        }

        //[HttpGet("/EditClauseVoice")]
        public async Task<IActionResult> EditClauseVoiceAsync(int id)
        {
            var clause = await _wordClauseService.GetWordClauseByIdAsync(id);
            if (clause == null)
                return RedirectToAction("Index");

            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != clause.UserId)
                return RedirectToAction("Index");

            ViewBag.Dictionaries = _dictService.GetAllDictionaries(false).Data;
            ViewBag.Model = clause;
            var voices = _wordClauseService.GetAudioFileForClausebyID(id);
            return View(voices);
        }

        public async Task<IActionResult> CreateVoice(int id, string arabClause, string otherClause, string dictName)
        {
            WordClauseVoiceViewModel model = new WordClauseVoiceViewModel();
            model.Id = id;
            model.ArabClause = arabClause;
            model.OtherClause = otherClause;
            model.DictName = dictName;
            CreateAudioFileForClauseDto file = new CreateAudioFileForClauseDto();
            ViewBag.Model = model;
            return View(file);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateWordClauseVoice(CreateAudioFileForClauseDto model)
        {
            var res = await _wordClauseService.CreateAudiFileForClause(model);
            if(res.IsSuccess)
            {
                return RedirectToAction("EditClauseVoice", new { id = model.ClauseId });
            }
            return View();
        }

        [HttpGet("/Registered/[controller]/Approve/{id}/{approve}/{clauseId}")]
        public async Task<IActionResult> Approve(int id, bool approve, int clauseId)
        {
            var r = await _wordClauseService.GetWordClauseByIdAsync(id);
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != r.UserId)
                return RedirectToAction("Index");

            var res = await _wordClauseService.ApproveVoice(id, approve);
            if (res.IsSuccess)
            {
                ViewBag.successs = "Success";
                return RedirectToAction("EditClauseVoice", new { id = clauseId });
            }
            return RedirectToAction("EditClauseVoice", new { id = clauseId });
        }

    }
}
