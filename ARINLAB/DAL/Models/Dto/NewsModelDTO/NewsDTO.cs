using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models.Dto.NewsModelDTO
{
    public class NewsDTO
    {
        public int Id { get; set; }

        [Required]
        public int NewsCategoryID { get; set; }
        [Required]
        public string Image { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsPublish { get; set; }
        public string NewsCategoryName { get; set; }
        public string ShortDate { get; set; }
        public string IdString { get; set; }
        [Required]
        public DateTime DatePublished { get; set; }
    }
}
