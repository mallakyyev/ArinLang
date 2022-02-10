using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto.Ratings
{
    public class WordClauseRating
    {
        
        public int WordClauseId { get; set; }
        public WordClause WordClause { get; set; }
        public int Rating { get; set; }
    }
}
