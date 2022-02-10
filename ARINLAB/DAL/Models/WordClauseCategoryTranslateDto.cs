using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WordClauseCategoryTranslateDto
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string LanguageCulture { get; set; }
        [Required]
        public int WordClauseCategoryId { get; set; } 
        
    }
}
