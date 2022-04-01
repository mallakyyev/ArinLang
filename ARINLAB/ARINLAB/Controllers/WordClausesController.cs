using ARINLAB.Services;
using ARINLAB.Services.ImageService;
using ARINLAB.Services.News;
using ARINLAB.Services.Ratings;
using ARINLAB.Services.SessionService;
using DAL.Models.Dto.NewsModelDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Controllers
{
    public class WordClausesController : Controller
    {
        private readonly UserDictionary _userDictionary;
        private readonly IWordClauseService _wordClauseService;
        private readonly IDictionaryService _dictService;
        private readonly IRatingService _ratingServices;
        private readonly IImageService _imageService;
        private readonly INewsService _newsService;
        public WordClausesController(UserDictionary userDict, IWordClauseService wordClauseService, 
                            IDictionaryService dictionaryService, IRatingService ratingServices,
                            IImageService imageService, INewsService newsService)
        {
            _userDictionary = userDict;
            _wordClauseService = wordClauseService;
            _dictService = dictionaryService;
            _ratingServices = ratingServices;
            _imageService = imageService;
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

        public async Task<IActionResult> Details(int id, string bag="")
        {
            //var clause = await _wordClauseService.GetWordClauseByIdAsync(id);
            var clause = await _wordClauseService.IncreaseViewed(id);
            if (clause == null)
                return RedirectToAction("Index");
            
            ViewBag.Rating = (int)Math.Round(_ratingServices.GetRatingForWordClause(id));
            ViewBag.Dictionaries = _dictService.GetAllDictionaries().Data;
            ViewBag.Model = clause;
            if (!string.IsNullOrEmpty(bag))
            {
                ViewBag.Bag = bag;
            }
            ViewBag.ExportImage = _imageService.PhraseExport(clause.ArabClause, clause.OtherReader,
                                                            clause.OtherClause, clause.ArabReader);
            var voices = _wordClauseService.GetAudioFileForClausebyID(id, true);
            ViewBag.News = (List<NewsDTO>)(_newsService.GetFourPublishNews().ToList());
            return View(voices);

        }

        
        public async Task<IActionResult> SetRatingAsync(float Rating, int WordClauseId)
        {
            var responce = await _ratingServices.SetRatingForWordClauseAsync(Rating, WordClauseId);
            try
            {
                var res = await _wordClauseService.GetWordClauseByIdAsync(WordClauseId);
                if (res != null)
                {
                    ViewBag.Dictionaries = _dictService.GetAllDictionaries().Data;
                    ViewBag.Model = res;
                    var voices = _wordClauseService.GetAudioFileForClausebyID(WordClauseId, true);
                    ViewBag.RatingResult = responce.IsSuccess;
                    ViewBag.Rating = _ratingServices.GetRatingForWordClause(WordClauseId);
                    return View("Details",voices);
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
    }
}
