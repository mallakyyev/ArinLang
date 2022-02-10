using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto.Ratings
{
    public class NamesRating
    {
        
        public int NamesId { get; set; }
        public Names Name { get; set; }
        public int Rating { get; set; }
    }
}
