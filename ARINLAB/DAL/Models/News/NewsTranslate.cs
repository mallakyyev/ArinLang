using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.News
{
    public class NewsTranslate
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public News News { get; set; }
        public string LanguageCulture { get; set; }
    }
}
