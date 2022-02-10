using DAL.Models.Dto;
using DAL.Models.Dto.NamesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Models
{
    public class SearchResultViewModel
    {
        public List<WordDto> Words { get; set; }
        public List<NamesDto> Names{ get; set; }
        public List<WordClauseDto> Clauses { get; set; }
    }
}
