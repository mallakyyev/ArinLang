using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class CreateAudioFileForClauseDto
    {
        public IFormFile ArabVoiceFile { get; set; }
        public IFormFile OtherVoiceFile { get; set; }

        public int ClauseId { get; set; }
        public bool IsApproved { get; set; }
    }
}
