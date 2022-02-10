using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto.Ratings
{
    public class NamesImageRating
    {
        
        public int NamesImageId { get; set; }
        public NameImages NameImage{ get; set; }
        public int Rating { get; set; }
    }
}
