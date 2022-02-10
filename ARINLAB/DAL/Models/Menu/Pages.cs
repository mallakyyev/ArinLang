using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Menu
{
    public class Pages
    {
        public int Id { get; set;  }
        public bool IsPublish { get; set; }
        public int? MenuId { get; set;  }
        public Menu Menu { get; set; }
        public ICollection<PagesTranslate> PagesTranslates { get; set; }
    }
}
