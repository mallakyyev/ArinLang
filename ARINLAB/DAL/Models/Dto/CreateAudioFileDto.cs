using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class CreateAudioFileDto
    {
        public string ArabVoice { get; set; }
        public string OtherVoice { get; set; }
        public int WordId { get; set; }
        public bool IsApproved { get; set; }
    }
}
