using DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Models
{
    public class StatisticCard
    {
        public WordType Type;
        public int totalCount { get; set; }
        public int Editers { get; set; } 
    }
    public class HomeViewModel
    {
        public List<StatisticCard> StatistiCards { get; set; }
        public List<WordDto> RandomWords { get; set; }
    }
}
