using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Dictionary
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string ArabTranslate { get; set; }
        public ICollection<Word> Words { get; set; }       
        public ICollection<WordClause> WordClauses { get; set; }
        public ICollection<Names> Names { get; set; }
    }
}
