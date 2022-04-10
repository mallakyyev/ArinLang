using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WordClause
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ArabClause { get; set; }
        public string OtherClause { get; set; }
        public string ArabReader { get; set; }
        public string OtherReader { get; set; }
        public int DictionaryId { get; set; }        
        public bool IsApproved { get; set; }
        public string UserId { get; set; }
        public int? Viewed { get; set; }
        public ICollection<AudioFileForClause> AudioFiles { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public WordClauseCategory WordClauseCategory { get; set; }
        public Dictionary Dictionary { get; set; }
        public ICollection<WordClauseRating> WordClauseRatings { get; set; }
        public DateTime AddedDate { get; set; }

    }
}
