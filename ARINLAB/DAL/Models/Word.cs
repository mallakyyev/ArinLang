using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string ArabWord { get; set; }
        public string OtherWord { get; set; }
        public int DictionaryId { get; set; }
        public string UserId { get; set; }
        public string ImageForShare { get; set; }
        public bool IsApproved { get; set; }

        public string ArabVoice { get; set; }
        public string OtherVoice { get; set; }
        public string ArabVoice1 { get; set; }
        public string OtherVoice1 { get; set; }
        public string ArabVoice2 { get; set; }
        public string OtherVoice2 { get; set; }
        public string ArabVoice3 { get; set; }
        public string OtherVoice3 { get; set; }
        public string ArabVoice4 { get; set; }
        public string OtherVoice4 { get; set; }

        public int? Viewed { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Dictionary Dictionary { get; set; }
        public ICollection<WordRating> WordRatings { get; set; }
        public ICollection<WordSentences> WordSentences { get; set; }
        public DateTime AddedDate { get; set; }

    }
}
