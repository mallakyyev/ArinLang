using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class CreateWordClauseCategoryDto
    {       
        
        [Required]
        public int ParentCategoryId { get; set; }

        [Required]        
        public List<WordClauseCategoryTranslate> WordClauseCategoryTranslates { get; set; }

    }
}
