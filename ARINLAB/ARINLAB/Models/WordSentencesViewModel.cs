using DAL.Models;
using DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Models
{
    public class WordSentencesViewModel
    {
        public WordDto Word { get; set; }
        public List<WordSentencesDto> WordSentences { get; set; }
        public List<AudioFile> AudioFiles { get; set; }

    }
}
