using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARINLAB.Services.News;
using ARINLAB.Services.NewsCategory;
using DAL.Models.Dto.NewsModelDTO;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ARINLAB.Areas.Admin.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsAPIController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly INewsCategoryService _categoryService;

        public NewsAPIController(INewsService newsService, INewsCategoryService categoryService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
        }

        // GET: api/NewsAPI
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return  DataSourceLoader.Load<NewsDTO>(_newsService.GetAllNews().AsQueryable(), loadOptions);
        }

        // GET: api/NewsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            NewsDTO pg = await _newsService.GetNewsByIdAsync(id);
            return Ok(pg);
        }
        // GET: api/NewsAPI/GetNewsByCategory/5
        [HttpGet("GetNewsByCategory/{id}")]
        public object GetNewsByCategory(DataSourceLoadOptions loadOptions, int catId)
        {
            return DataSourceLoader.Load<NewsDTO>(_newsService.GetNewsByCategory(catId).AsQueryable(), loadOptions);
        }

        // GET: api/NewsAPI/GetAllPublishNews
        [HttpGet("GetAllPublishNews")]
        public object GetAllPublishNews(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<NewsDTO>(_newsService.GetAllPublishNews().AsQueryable(), loadOptions);
        }

        // GET: api/NewsAPI/SortNewsByDate/{asc?}
        [HttpGet("SortNewsByDate/{asc?}")]
        public object SortNewsByDate(DataSourceLoadOptions loadOptions, bool asc = false)
        {
            return DataSourceLoader.Load<NewsDTO>(_newsService.SortNewsByDate(asc).AsQueryable(), loadOptions);
        }

        // GET: api/NewsAPI/SortNewsByDate/5/{asc?}
        [HttpGet("SortNewsByDate/{catId}/{asc?}")]
        public object SortNewsByDate(DataSourceLoadOptions loadOptions, int catId, bool asc = false)
        {
            return DataSourceLoader.Load<NewsDTO>(_newsService.SortNewsByDate(catId, asc).AsQueryable(), loadOptions);
        }

        // GET: api/NewsAPI/GetNewsByDate/{date}
        [HttpGet("GetNewsByDate/{date}")]
        public object GetNewsByDate(DataSourceLoadOptions loadOptions, DateTime d)
        {
            return DataSourceLoader.Load<NewsDTO>(_newsService.GetNewsByDate(d).AsQueryable(), loadOptions);
        }

        // GET: api/NewsAPI/GetNewsByDate/5/{date}
        [HttpGet("GetNewsByDate/{catId}/{date}")]
        public object GetNewsByDate(DataSourceLoadOptions loadOptions, int catId, DateTime d)
        {
            return DataSourceLoader.Load<NewsDTO>(_newsService.GetNewsByDateAndCategory(d, catId).AsQueryable(), loadOptions);
        }

        // PUT: api/NewsAPI/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] EditNewsDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _newsService.EditNews(value);
            return Ok(value);
        }

        // POST: api/NewsAPI
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromForm] CreateNewsDTO value)
        {
            if (ModelState.IsValid)
            {
                await _newsService.CreateNews(value);
                return Ok(value);
            }
            return BadRequest();
        }

        // DELETE: api/NewsAPI/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task DeleteAsync(int id)
        {
            await _newsService.RemoveNews(id);
        }

        // DELETE: api/NewsAPI/RemoveNewsByCategory/5 
        [HttpDelete("RemoveNewsByCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task RemoveNewsByCategory(int categoryID)
        {
            await _newsService.RemoveAllNews(categoryID);
        }

//-----------------------------------------------------------------------------------------------------
        // This part of the code will be handling NewsCategory service requests
//----------------------------------------------------------------------------------------------------- 

        // GET: api/NewsAPI/GetAllNewsCategories
        [HttpGet("GetAllNewsCategories")]
        public object GetAllNewsCategories(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<NewsCategoryDTO>(_categoryService.GetAllNewsCategory().AsQueryable(), loadOptions);
        }

        [HttpGet("GetAllNewsCategories/{id}")]
        public async Task<IActionResult> GetAllNewsCategories(int id)
        {
            NewsCategoryDTO pg = await _categoryService.GetNewsCategoryByIdAsync(id);
            return Ok(pg);
        }

        // POST: api/NewsAPI/CreateNewsCategory
        [HttpPost("CreateNewsCategory")]
        public async Task<IActionResult> CreateNewsCategory([FromForm] CreateNewsCategoryDTO value)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateNewsCategory(value);
                return Ok(value);
            }
            return BadRequest();
        }

        // PUT: api/NewsAPI/EditNewsCategory/5
        [HttpPut("EditNewsCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditNewsCategory([FromBody] EditNewsCategoryDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.EditNewsCategory(value);
            return Ok(value);
        }

        // DELETE: api/NewsAPI/DeleteNewsCategory/5
        [HttpDelete("GetAllNewsCategories/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task DeleteNewsCategory(int id)
        {
            await _categoryService.RemoveNewsCategory(id);
        }
    }
}
