using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ARINLAB.Services.Settings;
using DAL.Models;

namespace ARINLAB.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsAPIController : ControllerBase
    {
        private readonly ISettingsService _settingsService;


        public SettingsAPIController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
           
        }

        // GET: api/Admin/SettingsAPI
        [HttpGet]
        public object GetAsync(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<Settings>(_settingsService.GetAllSettings().AsQueryable(), loadOptions);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] Settings value)
        {
            if (ModelState.IsValid)
            {
                await _settingsService.CreateSettings(value);
                return Ok(value);
            }
            return BadRequest();
        }

      
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(int id, [FromBody] Settings value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (value.Id != id)
            {
                return BadRequest();
            }
            await _settingsService.EditSettings(value);
            return Ok(value);
        }

    
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task DeleteAsync(int id)
        {
            await _settingsService.RemoveSettings(id);
        }

    }
}