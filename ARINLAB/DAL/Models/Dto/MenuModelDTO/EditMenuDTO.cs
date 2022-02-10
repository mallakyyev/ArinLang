using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models.Dto.MenuModelDTO
{
    public class EditMenuDTO
    {
        public int Id { get; set; }

        public ICollection<MenuTranslateDTO> MenuTranslates { get; set; }

        public string Link { get; set; }

        [Required]
        public int Order { get; set; }

        public int? ParentId { get; set; }

        [Required]
        public bool IsPublish { get; set; }
    }
}
