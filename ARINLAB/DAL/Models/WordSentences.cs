using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WordSentences
    {
        public int Id { get; set; }
        public string ArabSentence { get; set; }
        public string OtherSentence { get; set; }
        public string ArabReader{ get; set; }
        public string OtherReader { get; set; }
        public int WordId { get; set; }
        public Word Word { get; set; }
        public string UserId { get; set; }
        public bool IsApproved { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<WordSentenceRating> WordSentenceRatings { get; set; }
    }
}
