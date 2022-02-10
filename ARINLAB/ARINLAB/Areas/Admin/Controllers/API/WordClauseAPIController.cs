using ARINLAB.Services;
using AutoMapper;
using DAL.Data;
using DAL.Models.Dto;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
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
    public class WordClauseAPIController : ControllerBase
    {
        private readonly IWordClauseService _wordClauseServices;
        private readonly IMapper _mapper;
        public WordClauseAPIController(IWordClauseService wordClauseService, IMapper mapper)
        {
            _wordClauseServices = wordClauseService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<object> GetAsync(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<WordClauseDto>((await _wordClauseServices.GetAllWordClausesAsync()).AsQueryable(), loadOptions);
        }
        

        [HttpGet("MyWord")]
        public async Task<object> GetWordByUser(DataSourceLoadOptions loadOptions)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return DataSourceLoader.Load<WordClauseDto>((await _wordClauseServices.GetAllWordClausesbyUserAsync(userId)).AsQueryable(), loadOptions);
        }

        [HttpGet("RandomWordClauses")]
        public object RandomWordClauses(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<WordClauseDto>(_wordClauseServices.GetRandomWordClauses(SD.Home_table_Count).AsQueryable(), loadOptions);
        }

        [HttpDelete("MyWord/{id}")]
        [Authorize(Roles = Roles.Registered)]
        public async Task DeleteMyWordAsync(int id)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var res = await _wordClauseServices.GetWordClauseByIdAsync(id);
            if (res.UserId == userId)               
                await _wordClauseServices.DeleteWordClause(id);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task DeleteAsync(int id)
        {
            await _wordClauseServices.DeleteWordClause(id);
        }

        [HttpGet("GetAllWordsClausesWithDict/{dictId}")]
        public object GetAllWordClausesWithDict(DataSourceLoadOptions loadOptions, int dictId)
        {
            return DataSourceLoader.Load<WordClauseDto>(_wordClauseServices.GetAllWordClausesWithDictId(dictId).AsQueryable(), loadOptions);
        }
    }
}
