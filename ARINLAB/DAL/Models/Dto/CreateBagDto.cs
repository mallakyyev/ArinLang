using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class CreateBagDto
    {
        public string Email { get; set; }
        public string Problem { get; set; }
        public string Link { get; set; }
    }
}
