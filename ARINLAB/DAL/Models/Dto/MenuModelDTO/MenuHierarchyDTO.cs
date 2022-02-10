using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Dto.MenuModelDTO
{
    public class MenuHierarchyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int Order { get; set; }
        public int? ParentId { set; get; }
        public bool IsPublish { get; set; }

        public List<MenuDTO> Childs { get; set; }
    }
}
