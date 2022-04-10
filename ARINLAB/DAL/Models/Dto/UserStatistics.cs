using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class UserStatistics
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int TotalWords { get; set; }
        public int TotalNames { get; set; }
        public int TotalPhrases { get; set; }
        public int TotalWordSentences { get; set; }
    }
}
