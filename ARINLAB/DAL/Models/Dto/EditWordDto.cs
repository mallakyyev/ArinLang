using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class EditWordDto
    {
        public int Id { get; set; }

        [Required]
        public string ArabWord { get; set; }        
        [Required]
        public string OtherWord { get; set; }        
        [Required]
        public int DictionaryId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        public IFormFile ArabVoiceForm { get; set; }
        public IFormFile OtherVoiceForm { get; set; }
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

        public string ImageForShare { get; set; }


    }
}
