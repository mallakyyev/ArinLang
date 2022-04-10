using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Models
{
    public class MailImageModel
    {
        public MailImageModel()
        {
            ImageSrc = new();
            Cid = new();
        }
        public List<string> ImageSrc { get; set; }
        public string MessageBody { get; set; }
        public List<string> Cid { get; set; }

    }
}
