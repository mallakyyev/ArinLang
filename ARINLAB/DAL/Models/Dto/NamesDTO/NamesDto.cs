﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto.NamesDTO
{
    public class NamesDto
    {
        public int Id { get; set; }
        [Required]
        public string ArabName { get; set; }
        [Required]
        public string OtherName { get; set; }
        [Required]
        public int DictionaryId { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        public string ImageForShare { get; set; }
        public string DictionaryName { get; set; }        
        public string UserId { get; set; }
        public string ArabVoice { get; set; }
        public string OtherVoice { get; set; }
        public IFormFile ArabForm{ get; set; }
        public DateTime AddedDate { get; set; }
        public IFormFile OtherForm { get; set; }
        public int? Viewed { get; set; }
        public ICollection<NameImages> NameImages { get; set; }

    }
}
