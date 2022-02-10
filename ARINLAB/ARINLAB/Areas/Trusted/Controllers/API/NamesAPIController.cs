using ARINLAB.Services;
using ARINLAB.Trusted.Admin.Controllers.API;
using DAL.Data;
using DAL.Models.Dto.NamesDTO;
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
    public class NamesAPIController : ControllerBase
    {
        private readonly INamesService _namesService;
        public NamesAPIController(INamesService namesService)
        {
            _namesService = namesService;
        }
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<NamesDto>(_namesService.GetAllNames().AsQueryable(), loadOptions);
        }

        [HttpGet("GetImage/{id}")]
        public object GetImageAsync(DataSourceLoadOptions loadOptions, int id)
        {
            return DataSourceLoader.Load<NameImagesDto>(_namesService.GetAllNamesImagesByNameId(id).AsQueryable(), loadOptions);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Trusted)]
        public async Task DeleteAsync(int id)
        {
            await _namesService.DeleteImageforNameAsync(id);
        }

        [HttpGet("GetRandomNames")]
        public object GetRandomNames(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<NamesDto>(_namesService.GetRandom_N_Names(SD.Home_table_Count).AsQueryable(), loadOptions);
        }

        [HttpGet("GetAllNamesWithDict/{dictId}")]
        public object GetAllNamesWithDict(DataSourceLoadOptions loadOptions, int dictId)
        {
            return DataSourceLoader.Load<NamesDto>(_namesService.GetAllNamesWithDictId(dictId).AsQueryable(), loadOptions);
        }


    }
}
