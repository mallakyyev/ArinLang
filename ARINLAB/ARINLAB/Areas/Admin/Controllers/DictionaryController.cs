using ARINLAB.Services;
using ARINLAB.Services.ImageService;
using DAL.Data;
using DAL.Models.Dto;
using DAL.Models.ResponceModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IImageService _imageService;
        public DictionaryController(IDictionaryService dictionaryService, IImageService imageService)
        {
            _dictService = dictionaryService;
            _imageService = imageService;
        }
        public IActionResult Index()
        {
            Responce result = _dictService.GetAllDictionaries(false);
            if (result.IsSuccess)
            {
                return View(new List<DAL.Models.Dictionary>((IEnumerable<DAL.Models.Dictionary>)result.Data));
            }
            return View(null);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateAsync(string dictName, string arabName, bool isActive, IFormFile FooterLogo)
        {
            var result = ((IEnumerable<DAL.Models.Dictionary>)_dictService.GetAllDictionaries(false).Data).Where(p => p.Language.CompareTo(dictName) == 0);
            if (result != null && result.Count() > 0 || string.IsNullOrEmpty(dictName))
                return RedirectToAction("Index");
            CreateDictionaryDto newDict = new CreateDictionaryDto();
            newDict.Language = dictName;
            newDict.ArabTranslate = arabName;
            newDict.IsActive = isActive;
            if (FooterLogo != null)
                newDict.FooterLogo = await _imageService._UploadImage(FooterLogo, "Logo");
            var responce = _dictService.CreateDictionary(newDict);
            return RedirectToAction("Index");
        }


        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var responce = await _dictService.DeleteDictionaryAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var dict = _dictService.GetDictionary(id);
            if(dict != null)
            {
                return View(dict);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditAsync(DAL.Models.Dictionary model)
        {
            if(model.File != null)
                model.FooterLogo = await _imageService._UploadImage(model.File, "Logo");
            var res = _dictService.EditDictionary(model);
           
            return RedirectToAction("Index");
        }
    }
}
