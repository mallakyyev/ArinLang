﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Models
{
    public class NamesImagesViewModel
    {
        public int Id { get; set; }
        public string ArabName{ get; set; }
        public string OtherName{ get; set; }
        public string DictName { get; set; }
        public string  ArabVoice { get; set; }
        public string OtherVoice { get; set; }
        public int? Viewed { get; set; }
    }
}
