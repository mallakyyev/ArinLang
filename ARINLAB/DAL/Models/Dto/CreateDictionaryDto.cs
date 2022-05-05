using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class CreateDictionaryDto
    {
        [Required]
        public string Language { get; set; }

        [Required]
        public string ArabTranslate { get; set; }

        [Required]
        public bool IsActive { get; set; }
        
        public string FooterLogo { get; set; }
    }
}
