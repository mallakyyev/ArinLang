using ARINLAB.Services;
using ARINLAB.Services.Ratings;
using ARINLAB.Services.SessionService;
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
        public WordClausesController(UserDictionary userDict, IWordClauseService wordClauseService, 
                            IDictionaryService dictionaryService, IRatingService ratingServices)
        {
            _userDictionary = userDict;
            _wordClauseService = wordClauseService;
            _dictService = dictionaryService;
            _ratingServices = ratingServices;
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

        public async Task<IActionResult> DetailsAsync(int id)
        {
            var clause = await _wordClauseService.GetWordClauseByIdAsync(id);
            if (clause == null)
                return RedirectToAction("Index");
            ViewBag.Rating = _ratingServices.GetRatingForWordClause(id);
            ViewBag.Dictionaries = _dictService.GetAllDictionaries().Data;
            ViewBag.Model = clause;
            var voices = _wordClauseService.GetAudioFileForClausebyID(id, true);
            return View(voices);

        }

        [HttpPost]
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
