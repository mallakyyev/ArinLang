using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class WordClauseCategoryDto
    {
        public int Id { get; set; }      
        public int ParentCategoryId { get; set; }            
        public string CategoryName { get; set; }
        public string ParenCategoryName { get; set; }

    }
}
