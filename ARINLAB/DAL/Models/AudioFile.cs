using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AudioFile
    {
        public int Id { get; set; }
        public string ArabVoice { get; set; }
        public string OtherVoice { get; set; }
        public int WordId { get; set; }
        public bool IsApproved { get; set; }
        public Word Word{ get; set; }
    }
}
