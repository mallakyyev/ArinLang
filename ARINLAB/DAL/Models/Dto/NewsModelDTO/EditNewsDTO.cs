using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models.Dto.NewsModelDTO
{
    public class EditNewsDTO
    {
        public int Id { get; set; }

        [Required]
        public int NewsCategoryID { get; set; }

        [Required]
        public ICollection<NewsTranslateDTO> NewsTranslates { get; set; }

        public IFormFile FormFile { get; set; }
        public string PictureName { get; set; }
        public bool DeleteImage { get; set; }

        [Required]
        public DateTime DatePublished { get; set; }

        [Required]
        public bool IsPublish { get; set; }
    }
}
