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
    public class BagApiController : ControllerBase
    {
        private readonly BagService _bagService;
        public BagApiController(BagService bagService)
        {
            _bagService = bagService;
        }
        // GET: api/<BagApiController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<Bag>(_bagService.GetAllBags().AsQueryable(), loadOptions);
        }

       
        // POST api/<BagApiController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void Post([FromBody] CreateBagDto value)
        {
            _bagService.CreateBag(value);
        }

       
        // DELETE api/<BagApiController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void Delete(int id)
        {
            _bagService.DeleteBag(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]   
        public void Bag(CreateBagDto bag) 
        {
            _bagService.CreateBag(bag);            
        }
    }
}
