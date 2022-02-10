using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WordRating
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public Word Word { get; set; }
        public float Rating { get; set; }
    }
}
