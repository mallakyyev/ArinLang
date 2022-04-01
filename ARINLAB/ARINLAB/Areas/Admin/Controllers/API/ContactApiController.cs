using ARINLAB.Services;
using DAL.Models;
using DAL.Models.Dto;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ARINLAB.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]   
    [Authorize(Roles = "Admin")]
    public class ContactApiController : ControllerBase
    {
        private readonly ContactService _ctService;
        public ContactApiController(ContactService contactService)
        {
            _ctService = contactService;
        }
        // GET: api/<ContactController>
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<Contact>(_ctService.GetAllContacts().AsQueryable(), loadOptions);
        }
       

        // POST api/<ContactController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateContactDto value)
        {
            if (ModelState.IsValid)
            {
                _ctService.CreateContact(value);
                return Ok(value);
            }
            return BadRequest();
        }
       

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _ctService.DeleteContact(id);
        }
    }
}
