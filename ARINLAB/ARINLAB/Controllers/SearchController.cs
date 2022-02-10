using ARINLAB.Services.SessionService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Controllers
{
    
    public class SearchModel
    {
        public string Term { get; set; }
        public int  DictionaryId { get; set; }
    }
    public class SearchController : Controller
    {
        private readonly UserDictionary _userDict;
        public SearchController(UserDictionary userDictionary)
        {
            _userDict = userDictionary;
        }
        
        //[HttpGet("Search/{term}")]
        public IActionResult Search(string term)
        {
            SearchModel model = new SearchModel()
            {
                Term = term,
                DictionaryId = _userDict.GetDictionaryId()
            };
            return View(model);
        }
    }
}
