using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ARINLAB.Services.Menu;
using DAL.Models.Dto.MenuModelDTO;

namespace ARINLAB.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuAPIController : ControllerBase
    {
        private readonly IMenuService _menuServece;
       
        public MenuAPIController(IMenuService menuService)
        {
            _menuServece = menuService;
          
        }
        // GET: api/MenuAPI
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<MenuDTO>(_menuServece.GetAllMenus().AsQueryable(), loadOptions);
        }

        // GET: api/MenuAPI/GetAllPublishMenu
        [HttpGet("GetAllPublishMenu")]
        public object GetAllPublishMenu(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<MenuDTO>(_menuServece.GetAllPublishMenus().AsQueryable(), loadOptions);
           
        }


        // GET: api/MenuAPI/GetAllMenuButThis/5
        [HttpGet("GetAllMenuButThis/{id}")]
        public object GetAllMenuButThis(DataSourceLoadOptions loadOptions,int id)
        {
            return DataSourceLoader.Load<MenuDTO>(_menuServece.GetAllMenuButThis(id)
                .AsQueryable(), loadOptions);
        }


        // GET: api/MenuAPI/GetMenuWithOutPage
        [HttpGet("GetMenuWithOutPage")]
        public object GetMenuWithOutPage(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<MenuDTO>(_menuServece.GetMenuWithOutPage()
                .AsQueryable(), loadOptions);
        }

        // GET: api/MenuAPI/GetMenuWithOutPage/{id}
        [HttpGet("GetMenuWithOutPage/{id}")]
        public object GetMenuWithOutPage(DataSourceLoadOptions loadOptions,int id)
        {
            return DataSourceLoader.Load<MenuDTO>(_menuServece.GetMenuWithOutPage(id)
                .AsQueryable(), loadOptions);
        }

        // GET: api/MenuAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MenuDTO menu = await _menuServece.GetMenuByIdAsync(id);
            return Ok(menu);
        }


        // POST: api/MenuAPI
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromForm] CreateMenuDTO value)
        {
            if (ModelState.IsValid)
            {
                await _menuServece.CreateMenu(value);
                return Ok(value);
            }
            return BadRequest();
        }

        // PUT: api/MenuAPI/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] EditMenuDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _menuServece.EditMenu(value);
            return Ok(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task DeleteAsync(int id)
        {
            await _menuServece.RemoveMenu(id);
        }
       
    }
}