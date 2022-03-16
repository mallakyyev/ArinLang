using ARINLAB.Models;
using ARINLAB.Services;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto.NamesDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ARINLAB.Areas.Admin.Controllers
{
    [Area(Roles.Admin)]
    [Authorize(Roles = Roles.Admin)]
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
                model.IsApproved = true;
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
            if (ModelState.IsValid)
            {
                model.NameImages = null;
                var res =  await _nameService.EditNameAsync(model);
                if (res.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public async Task<IActionResult> EditImageAsync(int id)
        {
            var clause = await _nameService.GetNameByIdAsync(id);
            if (clause == null)
                return RedirectToAction("Index");
            ViewBag.Dictionaries = _dictService.GetAllDictionaries().Data;
            ViewBag.Model = clause;
            var voices = _nameService.GetAllNamesImagesByNameIdAsync(id);
            return View(voices);
        }

        [HttpGet("/Admin/[controller]/AddImage/{id}")]
        public async Task<IActionResult> AddImageAsync(int id)
        {
            var res = await _nameService.GetNameByIdAsync(id);
            if (res == null)
                return RedirectToAction("EditImage", new { id = id });

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

        public async Task<IActionResult> EditApprove(int id, bool approve)
        {
            var res = await _nameService.ApproveImage(id, approve);
            if(res != null && res.IsSuccess)
            {
                return RedirectToAction("EditImageAsync", new { id = ((NameImages)res.Data).Id });
            }
            return View("Index");

        }


    }
}
