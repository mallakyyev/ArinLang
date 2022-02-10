using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WordClauseRating
    {
        public int Id { get; set; }
        public int WordClauseId { get; set; }
        public WordClause WordClause { get; set; }
        public float Rating { get; set; }
    }
}
