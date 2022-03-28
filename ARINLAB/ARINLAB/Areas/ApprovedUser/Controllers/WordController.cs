using ARINLAB.Models;
using ARINLAB.Services;
using ARINLAB.Services.ImageService;
using AutoMapper;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto;
using DAL.Models.ResponceModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ARINLAB.Areas.ApprovedUser.Controllers
{
    [Area("ApprovedUser")]
    [Authorize(Roles = Roles.Trusted)]
    public class WordController : Controller
    {
        private readonly IWordServices _wordsService;
        private readonly IDictionaryService _dictService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FileServices _audoFileServise;
        private readonly IImageService _fileService;        
        private readonly IMapper _mapper;
        public WordController(IWordServices wordServices, IDictionaryService dictionaryService, UserManager<ApplicationUser> userManager,
                              IMapper mapper, FileServices fileServices, IImageService imageService)
        {
            _wordsService = wordServices;
            _dictService = dictionaryService;
            _userManager = userManager;
            _mapper = mapper;
            _audoFileServise = fileServices;
            _fileService = imageService;
        }
        public IActionResult Index()
        {
           
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var result = new List<DAL.Models.Dictionary>((IEnumerable<DAL.Models.Dictionary>)_dictService.GetAllDictionaries().Data);
            ViewBag.Dicts = result;
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateAsync(CreateWordDto newW)
        {
            var r = new List<DAL.Models.Dictionary>((IEnumerable<DAL.Models.Dictionary>)_dictService.GetAllDictionaries().Data);
            if (newW != null)
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                newW.UserId = userId;
                newW.IsApproved = true;
                if (ModelState.IsValid)
                {
                    Responce result = await _wordsService.addWordAsync(newW);
                    if (result.IsSuccess)
                    {
                        ViewBag.text = $"Word {newW.ArabWord} <-> {newW.OtherWord} added successfully";
                        ViewBag.page = 1;
                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet("/ApprovedUser/[controller]/Edit/{id}")]
        public async Task<IActionResult> EditAsync(int id)
        {
            if (id < 0)
            {
                RedirectToAction("Index");
            }
            var res = await _wordsService.GetWordByIdAsync(id);
            if (res == null)
            {
                RedirectToAction("Index");
            }
            ViewBag.Dictionaries = new List<Dictionary>((IEnumerable<Dictionary>)_dictService.GetAllDictionaries().Data);
            return View(_mapper.Map<EditWordDto>(res));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(EditWordDto model)
        {
            try
            {
                Responce result = await _wordsService.editWordAsync(model);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Dictionaries = new List<Dictionary>((IEnumerable<Dictionary>)_dictService.GetAllDictionaries().Data);
                return View(model);
            }
        }
        [HttpGet("/ApprovedUser/[controller]/EditWord/{id}")]
        public async Task<IActionResult> EditWordAsync(int id)
        {
            try
            {
                var res = await _wordsService.GetWordByIdAsync(id);
                if (res != null)
                {
                    WordSentencesViewModel model = new();
                    model.Word = res;
                    model.WordSentences = _wordsService.GetAllWordSentencesByWordId(id);

                    ViewBag.dict = _dictService.GetAllDictionaries().Data;
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditWordSentenceAsync(WordSentencesDto model1)
        {
            try
            {
                var resu = await _wordsService.EditWordSentenceAsync(model1);
                if (resu.IsSuccess)
                {
                    try
                    {
                        var res = await _wordsService.GetWordByIdAsync(model1.WordId);
                        if (res != null)
                        {
                            WordSentencesViewModel model = new();
                            model.Word = res;
                            model.WordSentences = _wordsService.GetAllWordSentencesByWordId(model1.WordId);

                            ViewBag.dict = _dictService.GetAllDictionaries().Data;
                            ViewBag.text = "Success";
                            return View("EditWord", model);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View(model1);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error";
                return View(model1);
            }
        }

        public IActionResult AddWordSentence(int id, string arabWord, string otherWord, string dictId)
        {
            SimpleWordModel model = new SimpleWordModel
            {
                Id = id,
                ArabWord = arabWord,
                OtherWord = otherWord,
                DictID = dictId
            };
            ViewBag.dict = _dictService.GetAllDictionaries().Data;
            ViewBag.model = model;
            return View();
        }

        public async Task<IActionResult> EditWordSentenceAsync(int id, string arabWord, string otherWord, string dictId)
        {
            SimpleWordModel model = new SimpleWordModel
            {
                Id = id,
                ArabWord = arabWord,
                OtherWord = otherWord,
                DictID = dictId
            };
            ViewBag.dict = _dictService.GetAllDictionaries().Data;
            ViewBag.model = model;
            var sent = await _wordsService.GetWordSentencesById(id);
            return View(sent);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddWordSentenceAsync(CreateWordSentencesDto model1)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                model1.UserId = userId;
                model1.IsApproved = true;
                var resu = await _wordsService.CreateWordSentenceAsync(model1);
                if (resu.IsSuccess)
                {
                    try
                    {
                        var res = await _wordsService.GetWordByIdAsync(model1.WordId);
                        if (res != null)
                        {
                            WordSentencesViewModel model = new();
                            model.Word = res;
                            model.WordSentences = _wordsService.GetAllWordSentencesByWordId(model1.WordId);

                            ViewBag.dict = _dictService.GetAllDictionaries().Data;
                            ViewBag.text = "Success";
                            return View("EditWord", model);
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View(model1);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error";
                return View(model1);
            }
        }

        [HttpGet("/ApprovedUser/[controller]/Sentence/{id}/{approve}")]
        public async Task<IActionResult> EditApproveWordAsync(int id, bool approve)
        {
            var resp = await _wordsService.EditWordSentenceApproveByIdAsync(id, approve);

            if (resp.IsSuccess)
            {
                ViewBag.text = "Success";
            }
            else
            {
                ViewBag.text = "";
            }

            try
            {
                var res = await _wordsService.GetWordByIdAsync(((WordSentences)resp.Data).WordId);
                if (res != null)
                {
                    WordSentencesViewModel model = new();
                    model.Word = res;
                    model.WordSentences = _wordsService.GetAllWordSentencesByWordId(res.Id);
                    ViewBag.dict = _dictService.GetAllDictionaries().Data;
                    return View("EditWord", model);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }
        
        [HttpGet("/ApprovedUser/[controller]/DeleteSentence/{id}")]
        public async Task<IActionResult> DeleteWordSentence(int id)
        {
            var res = await _wordsService.DeleteSentence(id);
            if (res.IsSuccess)
            {
                try
                {
                    var res1 = await _wordsService.GetWordByIdAsync(((WordSentences)res.Data).WordId);
                    if (res1 != null)
                    {
                        WordSentencesViewModel model = new();
                        model.Word = res1;
                        model.WordSentences = _wordsService.GetAllWordSentencesByWordId(res1.Id);
                        ViewBag.dict = _dictService.GetAllDictionaries().Data;
                        ViewBag.text = "Success";
                        return View("EditWord", model);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        
    }
}
