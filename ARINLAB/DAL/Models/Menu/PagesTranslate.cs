using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Menu
{
    public class PagesTranslate
    {
        public int Id { get; set; }
        public int PagesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Pages Pages { get; set; }
        public string LanguageCulture { get; set; }

    }
}
