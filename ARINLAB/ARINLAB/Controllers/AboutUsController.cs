using ARINLAB.Services;
using ARINLAB.Services.News;
using DAL.Models.Dto.NewsModelDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly AboutService _service;
        private readonly INewsService _newsService;
        public AboutUsController(AboutService service, INewsService newsService)
        {
            _service = service;
            _newsService = newsService;
        }
        public IActionResult About()
        {
            ViewBag.News = (List<NewsDTO>)(_newsService.GetFourPublishNews().ToList());
            return View(_service.GetAboutus());
        }
    }
}
