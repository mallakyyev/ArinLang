using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.News
{
    public class News
    {
        public int Id { get; set; }
        public int NewsCategoryID { get; set; }
        public string Image { get; set; }
        public bool IsPublish { get; set; }
        public DateTime DatePublished { get; set; }
        public NewsCategory NewsCategory { get; set; }
        public ICollection<NewsTranslate> NewsTranslates { get; set; }
    }
}
