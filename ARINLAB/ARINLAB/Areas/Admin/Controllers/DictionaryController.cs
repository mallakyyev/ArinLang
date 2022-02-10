using ARINLAB.Services;
using DAL.Data;
using DAL.Models.Dto;
using DAL.Models.ResponceModel;
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
    public class DictionaryController : Controller
    {
        private readonly IDictionaryService _dictService;
        public DictionaryController(IDictionaryService dictionaryService)
        {
            _dictService = dictionaryService;
        }
        public IActionResult Index()
        {
            Responce result = _dictService.GetAllDictionaries();
            if (result.IsSuccess)
            {
                return View(new List<DAL.Models.Dictionary>((IEnumerable<DAL.Models.Dictionary>)result.Data));
            }
            return View(null);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(string dictName, string arabName)
        {
            var result = ((IEnumerable<DAL.Models.Dictionary>)_dictService.GetAllDictionaries().Data).Where(p => p.Language.CompareTo(dictName) == 0);
            if (result != null && result.Count() > 0 || string.IsNullOrEmpty(dictName))
                return RedirectToAction("Index");
            CreateDictionaryDto newDict = new CreateDictionaryDto();
            newDict.Language = dictName;
            newDict.ArabTranslate = arabName;
            var responce = _dictService.CreateDictionary(newDict);
            return RedirectToAction("Index");
        }


        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var responce = await _dictService.DeleteDictionaryAsync(id);
            return RedirectToAction("Index");
        }

    }
}
