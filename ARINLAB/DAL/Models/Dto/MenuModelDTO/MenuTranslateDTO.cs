using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models.Dto.MenuModelDTO
{
    public class MenuTranslateDTO
    {
        public int Id { get; set; }

        [Required]
        public int MenuId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string LanguageCulture { get; set; }
    }
}
