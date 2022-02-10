using ARINLAB.Services;
using ARINLAB.Trusted.Admin.Controllers.API;
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
using System.Threading.Tasks;

namespace ARINLAB.Areas.Trusted.Controllers.API
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

        [HttpGet("RandomWordClauses")]
        public object RandomWordClauses(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<WordClauseDto>(_wordClauseServices.GetRandomWordClauses(SD.Home_table_Count).AsQueryable(), loadOptions);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Trusted)]
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
