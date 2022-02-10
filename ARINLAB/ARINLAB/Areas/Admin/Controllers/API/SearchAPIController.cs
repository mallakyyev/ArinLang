using DAL.Models.Dto;
using DAL.Models.Dto.NamesDTO;
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
    public class SearchAPIController : ControllerBase
    {
        private readonly ARINLAB.Services.Search.ISearchService _searchService;
        public SearchAPIController(ARINLAB.Services.Search.ISearchService searchService)
        {
            _searchService = searchService;
        }
        
        [HttpGet("SearchWords/{term}/{dictId}")]
        public object SearchWords(DataSourceLoadOptions loadOptions, string term, int dictId)
        {
            return DataSourceLoader.Load<WordDto>(_searchService.SearchWords(term, dictId).AsQueryable(), loadOptions);
        }

        [HttpGet("SearchClauses/{term}/{dictId}")]
        public object SearchClauses(DataSourceLoadOptions loadOptions, string term, int dictId)
        {
            return DataSourceLoader.Load<WordClauseDto>(_searchService.SearchClauses(term, dictId).AsQueryable(), loadOptions);
        }


        [HttpGet("SearchNames/{term}/{dictId}")]
        public object SearchNames(DataSourceLoadOptions loadOptions, string term, int dictId)
        {
            return DataSourceLoader.Load<NamesDto>(_searchService.SearchNames(term, dictId).AsQueryable(), loadOptions);
        }
    }
}
