using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARINLAB.Services.Pages;
using DAL.Models.Dto.MenuModelDTO;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ARINLAB.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesAPIController : ControllerBase
    {
        private readonly IPagesService _pagesService;
        public PagesAPIController(IPagesService pagesService)
        {
            _pagesService = pagesService;
        }

        // GET: api/MenuAPI
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<PagesDTO>(_pagesService.GetAllPages().AsQueryable(), loadOptions);
        }

        // GET: api/PagesAPI/GetAllPublishPages
        [HttpGet("GetAllPublishPages")]
        public object GetAllPublishPages(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<PagesDTO>(_pagesService.GetAllIsPublishPages().AsQueryable(), loadOptions);
        }

        // GET: api/PagesAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            PagesDTO pg = await _pagesService.GetPageByMenuId(id);
            return Ok(pg);
        }


        // POST: api/PagesAPI
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromForm] CreatePagesDTO value)
        {
            if (ModelState.IsValid)
            {
                await _pagesService.CreatePages(value);
                return Ok(value);
            }
            return BadRequest();
        }

        // PUT: api/PagesAPI/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] EditPageDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _pagesService.EditPages(value);
            return Ok(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task DeleteAsync(int id)
        {
            await _pagesService.RemovePages(id);
        }
    }
}