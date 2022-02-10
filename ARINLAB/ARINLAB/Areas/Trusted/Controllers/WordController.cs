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
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ARINLAB.Areas.Trusted.Controllers
{
    [Area(Roles.Trusted)]
    [Authorize(Roles = Roles.Trusted)]
    public class WordController : Controller
    {
        private readonly IWordServices _wordsService;
        private readonly IDictionaryService _dictService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FileServices _audoFileServise;
        private readonly IImageService _fileService;
        private static string text = "";
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
            ViewBag.page = 1;          
            ViewBag.total = _wordsService.GetAllWords(1,int.MaxValue).Count;
            var result = _wordsService.GetAllWords(1, SD.pageSize);
            ViewBag.text = text;
            return View(new List<WordDto>(result));
        }

        [HttpGet("/Admin/[controller]/{id}/{approve}/{returnPage}")]
        public async Task<IActionResult> EditApproveAsync(int id, bool approve, int returnPage) {

            var resp = await _wordsService.EditWordApproveByIdAsync(id, approve);
           
                if (resp.IsSuccess)
                {
                    ViewBag.text = "Success";
                }
                else
                {
                    ViewBag.text = "";
                }
            
            ViewBag.page = returnPage;
            ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
            var res = _wordsService.GetAllWords(returnPage, SD.pageSize);           
            return View("Index", new List<WordDto>(res));
        }

        [HttpGet("/Admin/[controller]/Sentence/{id}/{approve}")]
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
                    model.AudioFiles = _audoFileServise.GetAudioFilesByWordId(res.Id);
                    ViewBag.dict = _dictService.GetAllDictionaries().Data;
                    return View("EditWord", model);
                }
                else
                {
                    ViewBag.page = 1;
                    ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                    var result = _wordsService.GetAllWords(1, SD.pageSize);
                    ViewBag.text = text;
                    return View("Index", result);
                }
            }
            catch (Exception e)
            {
                ViewBag.page = 1;
                ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                var result = _wordsService.GetAllWords(1, SD.pageSize);
                ViewBag.text = text;
                return View("Index", result);
            }
        }

        
        [HttpGet("/Admin/[controller]/DeleteSentence/{id}")]
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
                        model.AudioFiles = _audoFileServise.GetAudioFilesByWordId(res1.Id);
                        ViewBag.dict = _dictService.GetAllDictionaries().Data;
                        ViewBag.text = "Success";
                        return View("EditWord", model);
                    }
                    else
                    {
                        ViewBag.page = 1;
                        ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                        var result = _wordsService.GetAllWords(1, SD.pageSize);
                        ViewBag.text = text;
                        return View("Index", result);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.page = 1;
                    ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                    var result = _wordsService.GetAllWords(1, SD.pageSize);
                    ViewBag.text = text;
                    return View("Index", result);
                }
            }
            ViewBag.page = 1;
            ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
            var r = _wordsService.GetAllWords(1, SD.pageSize);
            ViewBag.text = text;
            return View("Index", r);
        }

        [HttpGet("/Admin/[controller]/List/{page}/{count}")]
        public IActionResult List(int page, int count)
        {
            var result = _wordsService.GetAllWords(page, count);
            ViewBag.text = "";
            ViewBag.page = page;           
            ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
            return View( "Index", new List<WordDto>(result));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var result = new List<DAL.Models.Dictionary>((IEnumerable<DAL.Models.Dictionary>)_dictService.GetAllDictionaries().Data);
            ViewBag.Dicts = result;
            return View(new CreateWordDto());
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
                        ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                        var rt = _wordsService.GetAllWords(1, SD.pageSize);
                        return View("Index", rt);
                    }
                }
                newW.IsApproved = false;
                ViewBag.Dicts = r;
                ViewBag.page = 1;
                ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                return View(newW);
            }
           
            ViewBag.Dicts = r;
            ViewBag.page = 1;
            ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
            return View(new CreateWordDto());
        }

        [HttpGet("/Admin/[controller]/Edit/{id}")]
        public async Task<IActionResult> EditAsync(int id)
        {
            if (id < 0) {
                ViewBag.page = 1;
                ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                var result = _wordsService.GetAllWords(1, SD.pageSize);
                ViewBag.text = text;
                return View("Index", new List<WordDto>(result));
            }
            var res = await _wordsService.GetWordByIdAsync(id);
            if(res == null )
            {
                ViewBag.page = 1;
                ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                var result = _wordsService.GetAllWords(1, SD.pageSize);
                ViewBag.text = text;
                return View("Index", new List<WordDto>(result));
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
                if (result.IsSuccess)
                {
                    ViewBag.page = 1;
                    ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                    var data = _wordsService.GetAllWords(1, SD.pageSize);
                    ViewBag.text = "Success";
                    ViewBag.Dictionaries = new List<Dictionary>((IEnumerable<Dictionary>)_dictService.GetAllDictionaries().Data);
                    return View("Index", new List<WordDto>(data));
                }
                else
                {
                    ViewBag.Dictionaries = new List<Dictionary>((IEnumerable<Dictionary>)_dictService.GetAllDictionaries().Data);
                    return View(model);
                }
            }catch(Exception e)
            {
                ViewBag.Dictionaries = new List<Dictionary>((IEnumerable<Dictionary>)_dictService.GetAllDictionaries().Data);
                return View(model);
            }
        }

        [HttpGet("/Admin/[controller]/Delete/{id}")]
        [Authorize(Roles=Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _wordsService.Delete(id);
            if (res.IsSuccess)
            {
                ViewBag.text = "Success";
            }
            ViewBag.page = 1;
            ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
            var data = _wordsService.GetAllWords(1, SD.pageSize);            
            ViewBag.Dictionaries = new List<Dictionary>((IEnumerable<Dictionary>)_dictService.GetAllDictionaries().Data);
            return View("Index", new List<WordDto>(data));
        }

     
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
                    model.AudioFiles = _audoFileServise.GetAudioFilesByWordId(id);
                    ViewBag.dict = _dictService.GetAllDictionaries().Data;
                    return View(model);
                }
                else
                {
                    ViewBag.page = 1;
                    ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                    var result = _wordsService.GetAllWords(1, SD.pageSize);
                    ViewBag.text = text;
                    return View("Index",result);
                }
            }catch(Exception e)
            {
                ViewBag.page = 1;
                ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                var result = _wordsService.GetAllWords(1, SD.pageSize);
                ViewBag.text = text;
                return View("Index", result);
            }
        }
        
        public IActionResult AddWordSentence(int id,string arabWord, string otherWord, string dictId)
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
                            model.AudioFiles = _audoFileServise.GetAudioFilesByWordId(model1.WordId);
                            ViewBag.dict = _dictService.GetAllDictionaries().Data;
                            ViewBag.text = "Success";
                            return View("EditWord", model);
                        }
                        else
                        {
                            ViewBag.page = 1;
                            ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                            var result = _wordsService.GetAllWords(1, SD.pageSize);
                            ViewBag.text = text;
                            return View("Index", result);
                        }
                    }
                    catch (Exception e)
                    {
                        ViewBag.page = 1;
                        ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                        var result = _wordsService.GetAllWords(1, SD.pageSize);
                        ViewBag.text = text;
                        return View("Index", result);
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
                            model.AudioFiles = _audoFileServise.GetAudioFilesByWordId(model1.WordId);
                            ViewBag.dict = _dictService.GetAllDictionaries().Data;
                            ViewBag.text = "Success";
                            return View("EditWord", model);
                        }
                        else
                        {
                            ViewBag.page = 1;
                            ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                            var result = _wordsService.GetAllWords(1, SD.pageSize);
                            ViewBag.text = text;
                            return View("Index", result);
                        }
                    }
                    catch (Exception e)
                    {
                        ViewBag.page = 1;
                        ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                        var result = _wordsService.GetAllWords(1, SD.pageSize);
                        ViewBag.text = text;
                        return View("Index", result);
                    }                                           
                }                
                else
                {
                    return View(model1);
                }
            }
            catch (Exception e) {
                ViewBag.Error = "Error";
                return View(model1);
            }           
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult EditWord(WordSentencesViewModel model)
        {
            return View(model);
        }

        public async Task<IActionResult> EditVoice(int id)
        {
            try
            {
                var res = await _wordsService.GetWordByIdAsync(id);
                if (res != null)
                {
                    WordSentencesViewModel model = new();
                    model.Word = res;
                    model.WordSentences = _wordsService.GetAllWordSentencesByWordId(id);
                    model.AudioFiles = _audoFileServise.GetAudioFilesByWordId(id);
                    ViewBag.dict = _dictService.GetAllDictionaries().Data;
                    return View(model);
                }
                else
                {
                    ViewBag.page = 1;
                    ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                    var result = _wordsService.GetAllWords(1, SD.pageSize);
                    ViewBag.text = text;
                    return View("Index", result);
                }
            }
            catch (Exception e)
            {
                ViewBag.page = 1;
                ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                var result = _wordsService.GetAllWords(1, SD.pageSize);
                ViewBag.text = text;
                return View("Index", result);
            }
        }


        public IActionResult AddWordVoice(int id, string arabWord, string otherWord, string dictId)
        {
            SimpleWordVoiceModel model = new SimpleWordVoiceModel            
            {
                Id = id,
                ArabWord = arabWord,
                OtherWord = otherWord,
                DictID = dictId,
            };
            ViewBag.dict = _dictService.GetAllDictionaries().Data;
            ViewBag.model = model;
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddWordVoiceAsync(SimpleWordVoiceModel model)
        {
            if(model.OtherVoice == null && model.ArabVoice == null)
            {
                return View("AddWordVoice", new { id = model.Id, otherWord = model.OtherWord, arabWord = model.ArabWord, dictId = model.DictID });
            }
            CreateAudioFileDto file = new CreateAudioFileDto
            {
                WordId = model.Id
            };
            if (model.ArabVoice != null)          
                file.ArabVoice = await _fileService.UploadImage(model.ArabVoice, SD.wordFilePath);
            if(model.OtherVoice != null)
                file.OtherVoice = await _fileService.UploadImage(model.OtherVoice, SD.wordFilePath);
            Responce res = await _audoFileServise.CreateAudioFileAsync(file);
            if(res.IsSuccess)
            {
                ViewBag.successs = "Success";
                try
                {
                    var res1 = await _wordsService.GetWordByIdAsync(model.Id);
                    if (res != null)
                    {
                        WordSentencesViewModel model1 = new();
                        model1.Word = res1;
                        model1.WordSentences = _wordsService.GetAllWordSentencesByWordId(model.Id);
                        model1.AudioFiles = _audoFileServise.GetAudioFilesByWordId(model.Id);
                        ViewBag.dict = _dictService.GetAllDictionaries().Data;
                        return View("EditVoice", model1);
                    }
                    else
                    {
                        ViewBag.page = 1;
                        ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                        var result = _wordsService.GetAllWords(1, SD.pageSize);
                        ViewBag.text = text;
                        return View("Index", result);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.page = 1;
                    ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                    var result = _wordsService.GetAllWords(1, SD.pageSize);
                    ViewBag.text = text;
                    return View("Index", result);
                }
            }
            _fileService.DeleteImage(file.ArabVoice);
            _fileService.DeleteImage(file.OtherVoice);
            ViewBag.Error = "Could not add";
            return View(model);
        }

        [HttpGet("/Admin/[controller]/DeleteVoice/{id}/{page}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteWord(int id, int page)
        {
            var res = await _audoFileServise.DeleteVoiceFile(id);
            if (res.IsSuccess)
            {
                ViewBag.successs = "Success";
                try
                {
                    var res1 = await _wordsService.GetWordByIdAsync(page);
                    if (res != null)
                    {
                        WordSentencesViewModel model1 = new();
                        model1.Word = res1;
                        model1.WordSentences = _wordsService.GetAllWordSentencesByWordId(page);
                        model1.AudioFiles = _audoFileServise.GetAudioFilesByWordId(page);
                        ViewBag.dict = _dictService.GetAllDictionaries().Data;
                        return View("EditVoice", model1);
                    }
                    else
                    {
                        ViewBag.page = 1;
                        ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                        var result = _wordsService.GetAllWords(1, SD.pageSize);
                        ViewBag.text = text;
                        return View("Index", result);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.page = 1;
                    ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
                    var result = _wordsService.GetAllWords(1, SD.pageSize);
                    ViewBag.text = text;
                    return View("Index", result);
                }
            }
            ViewBag.page = 1;
            ViewBag.total = _wordsService.GetAllWords(1, int.MaxValue).Count;
            var data = _wordsService.GetAllWords(1, SD.pageSize);
            ViewBag.Dictionaries = new List<Dictionary>((IEnumerable<Dictionary>)_dictService.GetAllDictionaries().Data);
            return View("Index", new List<WordDto>(data));
        }

    }
}
