using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.News
{
    public class NewsCategoryTranslate
    {
        public int Id { set; get; }
        public int NewsCategoryId { get; set; }
        public string Name { get; set; }
        public string LanguageCulture { get; set; }
        public NewsCategory NewsCategory { get; set; }
    }
}
