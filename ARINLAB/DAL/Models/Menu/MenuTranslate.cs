using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Menu
{
    public class MenuTranslate
    {
        public int Id { get; set; }
        public int MenuId { get; set;  }
        public string Name { get; set; }
        public string LanguageCulture { get; set;  }
        public Menu Menu { get; set; }
    }
}
