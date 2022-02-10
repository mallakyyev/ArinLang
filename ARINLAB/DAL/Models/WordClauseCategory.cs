using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WordClauseCategory
    {
        public int Id { get; set; }                
        public int ParentCategoryId { get; set; }                
        public ICollection<WordClauseCategoryTranslate> WordClauseCategoryTranslates { get; set; }
        public ICollection<WordClause> WordClauses { get; set; }
    }
}
