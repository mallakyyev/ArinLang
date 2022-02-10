using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto.NamesDTO
{
    public class CreateNameImagesDto
    {       
        public IFormFile ImageUri { get; set; }
        public int NamesId { get; set; }
        public bool IsApproved { get; set; }
        public string UserId { get; set; }
    }
}
