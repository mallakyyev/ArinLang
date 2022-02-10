using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models.Dto.MenuModelDTO
{
    public class PagesDTO
    {
        public int Id { get; set; }
        
        public int? MenuId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public string MenuIdName { get; set; }

        [Required]
        public bool IsPublish { get; set; }
    }
}
