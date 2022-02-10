using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto.Ratings
{
    public class WordSentenceRating
    {
        
        public int WordSentenceId { get; set; }
        public WordSentences WordSentence { get; set; }
        public int Rating { get; set; }
    }
}
