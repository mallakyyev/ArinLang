using ARINLAB.Services;
using ARINLAB.Services.Statistic;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Areas.ApprovedUser.Controllers
{
    [Area("ApprovedUser")]
    [Authorize(Roles = Roles.Trusted)]
    public class ApprovedUserController : Controller
    {
        private readonly IWordServices _wordsService;
        private readonly IStatisticsService _statService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ApprovedUserController(IWordServices wordServices, IStatisticsService statisticsService, UserManager<ApplicationUser> userM)
        {
            _statService = statisticsService;
            _wordsService = wordServices;
            _userManager = userM;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var res = _statService.GetMyStatisticsCard((await _userManager.GetUserAsync(User)).Id);
            return View(res);
        }
        
        public async Task<IActionResult> WordsAsync()
        {
            var res = (await _userManager.GetUserAsync(User)).Id;
            List<WordDto> _words = _wordsService.GetAllWords(res, 15,10);
            return View(_words);
        }
    }
}
