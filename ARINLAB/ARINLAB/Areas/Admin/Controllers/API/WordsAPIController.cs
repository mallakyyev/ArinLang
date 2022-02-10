using DAL.Models.Dto;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ARINLAB.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsAPIController : ControllerBase
    {
        private readonly ARINLAB.Services.IWordServices _wordService;
        public WordsAPIController(ARINLAB.Services.IWordServices wordService)
        {
            _wordService = wordService;
        }
        [HttpGet("RandomWords")]
        public object RandomWords(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<WordDto>(_wordService.GetRandom_N_Words(SD.Home_table_Count).AsQueryable(), loadOptions);
        }
        
        public object Get(DataSourceLoadOptions loadOptions)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return DataSourceLoader.Load<WordDto>(_wordService.GetAllWordsByUserId(userId).AsQueryable(), loadOptions);
        }
        

        [HttpGet("GetAllWordsWithDict/{dictId}")]
        public object GetAllNamesWithDict(DataSourceLoadOptions loadOptions, int dictId)
        {
            return DataSourceLoader.Load<WordDto>(_wordService.GetAllWordsWithDictId(dictId).AsQueryable(), loadOptions);
        }
    }
}
