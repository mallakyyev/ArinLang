using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class LogoDto
    {
        public int Id1 { get; set; }
        public string Name1 { get; set; }
        public string Link1 { get; set; }
        public string Image1 { get; set; }
        public IFormFile ImageForm1 { get; set; }

        public int Id2 { get; set; }
        public string Name2 { get; set; }
        public string Link2 { get; set; }
        public string Image2 { get; set; }
        public IFormFile ImageForm2 { get; set; }
    }
}
