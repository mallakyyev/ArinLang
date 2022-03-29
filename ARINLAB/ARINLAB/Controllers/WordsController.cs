using ARINLAB.Models;
using ARINLAB.Services;
using ARINLAB.Services.ImageService;
using ARINLAB.Services.News;
using ARINLAB.Services.Ratings;
using ARINLAB.Services.SessionService;
using AutoMapper;
using DAL.Models;
using DAL.Models.Dto;
using DAL.Models.Dto.NewsModelDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Controllers
{
    public class WordsController : Controller
    {
        private readonly UserDictionary _userDictionary;
        private readonly IWordServices _wordsService;
        private readonly Services.IDictionaryService _dictService;        
        private readonly FileServices _audoFileServise;
        private readonly IImageService _imageService;
        private readonly IRatingService _ratingServices;       
        private readonly IMapper _mapper;
        private readonly ILogger<WordsController> _logger;
        private readonly INewsService _newsService;
        public WordsController(UserDictionary userDictionary, IWordServices wordServices, Services.IDictionaryService dictionaryService
                                , FileServices fileServices, IRatingService ratingServices, IImageService imageService,
                                IMapper mapper, ILogger<WordsController> logger, INewsService newsService)
        {
            _userDictionary = userDictionary;
            _wordsService = wordServices;
            _dictService = dictionaryService;
            _audoFileServise = fileServices;
            _ratingServices = ratingServices;
            _imageService = imageService;
            _mapper = mapper;
            _logger = logger;
            _newsService = newsService;
        }
        public IActionResult Indexall()
        {
            var res = _userDictionary.GetDictionaryId();
            UserDictionaryModel model = new UserDictionaryModel();
            if ((int)res == -1)
            {
                model.DictionaryId = 1;
            }
            else
            {
                model.DictionaryId = (int)res;
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id, bool ratingResult = false)
        {
            try
            {                
                var res = await _wordsService.IncreaseViewed(id);
                //var res = await _wordsService.GetWordByIdAsync(id);
                if (res != null)
                {
                    res.ImageForShare = _imageService.CreateImageForExport(res.ArabWord, res.OtherWord);
                    //await _wordsService.editWordAsync(_mapper.Map<EditWordDto>(res));
                    WordSentencesViewModel model = new();
                    model.Word = res;
                    model.WordSentences = _wordsService.GetAllWordSentencesByWordId(id);
                    //model.AudioFiles = _audoFileServise.GetAudioFilesByWordId(id);
                    ViewBag.dict = _dictService.GetAllDictionaries().Data;
                    ViewBag.Rating = (int)Math.Round(_ratingServices.GetRatingForWord(id));
                    ViewBag.RatingResult = ratingResult;
                    ViewBag.ExportImage = res.ImageForShare;
                   
                    ViewBag.News = (List<NewsDTO>)(_newsService.GetFourPublishNews().ToList());
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Indexall");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Indexall");
            }
        }

        //[HttpGet("Share/{id}")]
        public async Task<IActionResult> Share(int id)
        {
            try
            {
                var res = await _wordsService.GetWordByIdAsync(id);
                
                if (res != null)
                {
                   // if (string.IsNullOrEmpty(res.ImageForShare))
                   // {
                        _logger.LogInformation($"Inside image null block");
                        res.ImageForShare = _imageService.CreateImageForExport(res.ArabWord, res.OtherWord);
                        _logger.LogInformation($"Saved image at {res.ImageForShare}.");
                        await _wordsService.editWordAsync(_mapper.Map<EditWordDto>(res));
                        _logger.LogInformation($"Saved in database.");
                   // }                    
                    //return Redirect($"~{res.ImageForShare}");
                }
                return RedirectToAction("Indexall");
            }catch(Exception e)
            {
                return RedirectToAction("Indexall");
            }
        } 
        
        public async Task<IActionResult> SetRatingAsync(float Rating, int WordId)
        {
            var responce = await _ratingServices.SetRatingForWordAsync(Rating, WordId);
            try
            {
                var res = await _wordsService.GetWordByIdAsync(WordId);
                
                if (res != null)
                {
                    WordSentencesViewModel model = new();
                    model.Word = res;
                    model.WordSentences = _wordsService.GetAllWordSentencesByWordId(WordId);
                    //model.AudioFiles = _audoFileServise.GetAudioFilesByWordId(WordId);
                    ViewBag.dict = _dictService.GetAllDictionaries().Data;
                    ViewBag.RatingResult = responce.IsSuccess;
                    ViewBag.Rating = _ratingServices.GetRatingForWord(WordId);
                    return RedirectToAction("Details", new { id = WordId, ratingResult = responce.IsSuccess });
                }
                else
                {
                    return RedirectToAction("Indexall");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Indexall");
            }           
        }
    }
}
