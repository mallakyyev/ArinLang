using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Menu
{
    public class Menu
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int Order { get; set; }
        public int? ParentId { set; get; }
        public Pages Pages { get; set; }
        public bool IsPublish { get; set; }

        public Menu ParentMenu { get; set; }

        public ICollection<Menu> Menus { get; set; }

        public ICollection<MenuTranslate> MenuTranslates { get; set; }    
    }
}
