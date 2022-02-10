using ARINLAB.Models;
using DAL.Models.Dto;
using DAL.Models.Dto.NamesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services.Search
{
    public interface ISearchService
    {
        public List<WordDto> SearchWords(string term, int dictId);
        public List<NamesDto> SearchNames(string term, int dictId);
        public List<WordClauseDto> SearchClauses(string term, int dictId);
    }
}
