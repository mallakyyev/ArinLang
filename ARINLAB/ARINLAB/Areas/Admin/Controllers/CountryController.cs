using ARINLAB.Services;
using DAL.Data;
using DAL.Models.Dto;
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
    public class CountryController : Controller
    {
        private readonly CountryService _countryService;

        public CountryController(CountryService countryService)
        {
            _countryService = countryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CountryDto model)
        {
            if (ModelState.IsValid)
            {
               _ = await _countryService.Create(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
       
        public IActionResult Edit([FromQuery] int id)
        {
            var model = _countryService.GetCountryById(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(CountryDto model)
        {
            if (ModelState.IsValid)
            {
                _ = await _countryService.EditAsync(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
