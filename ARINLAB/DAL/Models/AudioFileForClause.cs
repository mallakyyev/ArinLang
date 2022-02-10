using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AudioFileForClause
    {
        public int Id { get; set; }
        public string ArabVoice { get; set; }
        public string OtherVoice { get; set; }
        public int ClauseId{ get; set; }
        public bool IsApproved { get; set; }
        public WordClause WordClause{ get; set; }
    }
}
