using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models.Dto.MenuModelDTO
{
    public class MenuDTO
    {
        public int Id { get; set; }

        public string Link { get; set; }

        public string Name { get; set; }               
    
        public int Order { get; set; }
        
        public int? ParentId { set; get; }

        public string ParentIdName { get; set; }

        public bool IsPublish { get; set; }

        public PagesDTO Pages { get; set; }

        public List<MenuDTO> Childs { get; set; }
    }
}
