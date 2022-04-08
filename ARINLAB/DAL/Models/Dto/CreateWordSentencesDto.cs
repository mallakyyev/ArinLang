using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class CreateWordSentencesDto
    {
        [Required]
        public string ArabSentence { get; set; }

        [Required]
        public string OtherSentence { get; set; }
        public string ArabReader { get; set; }
        public string OtherReader { get; set; }
        
        [Required]
        public int WordId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public bool IsApproved { get; set; }
        public IFormFile ArabVoiceForm { get; set; }
        public IFormFile OtherVoiceForm { get; set; }
        public string ArabVoice { get; set; }
        public string OtherVoice { get; set; }
    }
}
