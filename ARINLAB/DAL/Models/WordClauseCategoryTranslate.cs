using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WordClauseCategoryTranslate
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string LanguageCulture { get; set; }
        public int WordClauseCategoryId { get; set; } 
        public WordClauseCategory WordClauseCategory { get; set; }
    }
}
