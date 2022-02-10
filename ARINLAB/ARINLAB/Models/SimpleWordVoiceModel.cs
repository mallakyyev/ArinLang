using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Models
{
    public class SimpleWordVoiceModel
    {
        public int Id { get; set; }
        public string ArabWord { get; set; }
        public string OtherWord { get; set; }
        public string DictID { get; set; }
        public IFormFile ArabVoice{ set; get; }
        public IFormFile OtherVoice { get; set; }
    }
}
