using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class LanguageDto
    {
        [Required]
        public string Culture { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsPublish { get; set; }

        public string FlagImage { get; set; }

        public int DisplayOrder { get; set; }
    }
}
