using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Models
{
    public class WordClauseVoiceViewModel
    {
        public int Id { get; set; }
        public string ArabClause{ get; set; }
        public string OtherClause { get; set; }
        public string DictName { get; set; }
    }
}
