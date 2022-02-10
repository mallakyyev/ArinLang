using ARINLAB.Services;
using ARINLAB.Trusted.Admin.Controllers.API;
using AutoMapper;
using DAL.Models.Dto;
using DevExtreme.AspNet.Data;
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
    public class WordClauseAudioAPIController : ControllerBase
    {
        private readonly IWordClauseService _wordClauseService;
        private readonly IMapper _mapper;

        public WordClauseAudioAPIController(IWordClauseService wordClauseService, IMapper mapper)
        {
            _wordClauseService = wordClauseService;
            _mapper = mapper;
        }

        [HttpGet("/{id}")]
        public  object Get(DataSourceLoadOptions loadOptions, int id)
        {
            return DataSourceLoader.Load<AudioFileForClauseDto>((_wordClauseService.GetAudioFileForClausebyID(id)).AsQueryable(), loadOptions);
        }
    }
}
