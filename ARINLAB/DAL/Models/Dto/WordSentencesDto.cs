using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class WordSentencesDto
    {
        public int Id { get; set; }
        public string ArabSentence { get; set; }
        public string OtherSentence { get; set; }
        public string ArabReader { get; set; }
        public string OtherReader { get; set; }
        public IFormFile ArabVoiceForm { get; set; }
        public IFormFile OtherVoiceForm { get; set; }
        public string ArabVoice { get; set; }
        public string OtherVoice { get; set; }
        public DateTime AddedDate { get; set; }

        public int WordId { get; set; }
        public string UserId { get; set; }
        public bool IsApproved { get; set; }
       
    }
}
