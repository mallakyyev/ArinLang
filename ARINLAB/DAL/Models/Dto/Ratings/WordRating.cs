using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto.Ratings
{
    public class WordRating
    {
        
        public int WordId { get; set; }
        public Word Word { get; set; }
        public int Rating { get; set; }
    }
}
