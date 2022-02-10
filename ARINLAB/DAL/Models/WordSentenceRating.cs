using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WordSentenceRating
    {
        public int Id { get; set; }
        public int WordSentenceId { get; set; }
        public WordSentences WordSentence { get; set; }
        public float Rating { get; set; }
    }
}
