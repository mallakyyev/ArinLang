using ARINLAB.Services;
using ARINLAB.Services.ImageService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ARINLAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportAPIController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ExportAPIController(IImageService imageService)
        {
            _imageService = imageService;
        }
        // GET: api/<ExportAPIController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ExportAPIController>/5
        [HttpGet("{first}/{second}")]
        public string Get(string first, string second)
        {
            return _imageService.CreateImageForExport(first, second);
        }

        [HttpGet("Phrase/{first}/{second}/{third}/{fourth}")]
        public string Phrase(string first, string second, string third, string fourth)
        {
            return _imageService.PhraseExport(first.ReverseOnlyNumbers(), second, third, fourth.ReverseOnlyNumbers());
        }
        // POST api/<ExportAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ExportAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ExportAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
