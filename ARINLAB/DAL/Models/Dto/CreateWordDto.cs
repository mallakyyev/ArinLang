using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class CreateWordDto
    {  
        [Required]
        public string ArabWord { get; set; }
        
        [Required]
        public string OtherWord { get; set; }
        
        [Required]
        public int DictionaryId { get; set; }
       
        public string UserId { get; set; }

        [Required]
        public bool IsApproved { get; set; }

    }
}
