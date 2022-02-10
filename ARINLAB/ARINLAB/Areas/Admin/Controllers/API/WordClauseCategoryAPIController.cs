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
using System.Threading.Tasks;

namespace ARINLAB.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class WordClauseCategoryAPIController : ControllerBase
    {
        private readonly IWordClauseService _wordClauseServices;
        private readonly IMapper _mapper;
        public WordClauseCategoryAPIController(IWordClauseService wordClauseService, IMapper mapper)
        {
            _wordClauseServices = wordClauseService;
            _mapper = mapper;
        }
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<WordClauseCategoryDto>(_wordClauseServices.GetAllWordClauseCategories().AsQueryable(), loadOptions);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task DeleteAsync(int id)
        {
            await _wordClauseServices.DeleteWordClauseCategory(id);
        }
    }
}
