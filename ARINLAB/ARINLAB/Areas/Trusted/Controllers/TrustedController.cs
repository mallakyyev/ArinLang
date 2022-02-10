using ARINLAB.Services;
using ARINLAB.Services.Statistic;
using DAL.Data;
using DAL.Models.Dto;
using Microsoft.AspNetCore.Authorization;
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
    public class TrustedController : Controller
    {
        private readonly IWordServices _wordsService;
        private readonly IStatisticsService _statService;
        
        public TrustedController(IWordServices wordServices, IStatisticsService statisticsService)
        {
            _statService = statisticsService;
            _wordsService = wordServices;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var res = _statService.GetMyStatisticsCard(userId);
            return View(res);
        }
       
        public IActionResult Words()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<WordDto> _words = _wordsService.GetAllWords(userId, 15,10);
            return View(_words);
        }
    }
}
