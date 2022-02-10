using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models.Dto
{
    public class CreateLanguageDTO
    {
        [Required]
        public string Culture { get; set; }

        [Required]
        public string Name { get; set; }

        public string FlagImage { get; set; }

        [Required]
        public bool Published { get; set; }

        public int DisplayOrder { get; set; }
    }
}
