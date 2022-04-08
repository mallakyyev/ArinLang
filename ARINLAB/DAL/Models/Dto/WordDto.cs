using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class WordDto
    {
        public int Id { get; set; }
        public string ArabWord { get; set; }
        public string OtherWord { get; set; }
        public int DictionaryId { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public bool IsApproved { get; set; }
        public string ImageForShare { get; set; }
        public string ArabVoice { get; set; }
        public int? Viewed { get; set; }
        public string OtherVoice { get; set; }                 
        public string Dictionary { get; set; }
        public int Number { get; set; }
        public ICollection<WordSentencesDto> WordSentences { get; set; }
    }
}
