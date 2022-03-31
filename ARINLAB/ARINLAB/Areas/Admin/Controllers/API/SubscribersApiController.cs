using ARINLAB.Services.Subscribe;
using DAL.Models.Email;
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
    public class SubscribersApiController : ControllerBase
    {
        private readonly ISubscribeService _subService;

        public SubscribersApiController(ISubscribeService subscribeService)
        {
            _subService = subscribeService;
        }
        // GET: api/<Subscribers>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<Subscribers>(_subService.GetAllSubscribers().AsQueryable(), loadOptions);
        }       
        

        // DELETE api/<Subscribers>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void Delete(string id)
        {
            _subService.DeleteSubscriber(id);
        }
    }
}
