using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Dto.NewsModelDTO
{
    public class EditNewsCategoryDTO
    {
        public int Id { get; set; }
        public ICollection<NewsCategoryTranslateDTO> NewsCategoryTranslates { get; set; }
    }
}
