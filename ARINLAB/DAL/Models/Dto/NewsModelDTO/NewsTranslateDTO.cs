using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models.Dto.NewsModelDTO
{
    public class NewsTranslateDTO
    {
        public int Id { get; set; }

        [Required]
        public int NewsId { get; set; }

        [Required]
        public string Name { get; set; }
        
       

        public string Description { get; set; }

        [Required]
        public string LanguageCulture { get; set; }
    }
}
