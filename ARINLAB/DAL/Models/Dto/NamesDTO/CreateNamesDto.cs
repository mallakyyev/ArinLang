using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto.NamesDTO
{
    public class CreateNamesDto
    {        
        [Required]
        public string ArabName { get; set; }
        [Required]
        public string OtherName { get; set; }
        public int DictionaryId { get; set; }
        public bool IsApproved { get; set; }
        public string ArabVoice { get; set; }
        public string OtherVoice { get; set; }
        public IFormFile ArabForm{ get; set; }
        public IFormFile OtherForm { get; set; }
        public string UserId { get; set; }
    }
}
