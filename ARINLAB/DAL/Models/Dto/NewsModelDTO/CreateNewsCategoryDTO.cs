using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Dto.NewsModelDTO
{
    public class CreateNewsCategoryDTO
    {
        public ICollection<NewsCategoryTranslateDTO> NewsCategoryTranslates { get; set; }
    }
}
