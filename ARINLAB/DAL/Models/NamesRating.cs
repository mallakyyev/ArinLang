using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class NamesRating
    {
        public int Id { get; set; }
        public int NamesId { get; set; }
        public Names Name { get; set; }
        public float Rating { get; set; }
    }
}
