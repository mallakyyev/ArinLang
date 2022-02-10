using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class NamesImageRating
    {
        public int Id { get; set; }
        public int NamesImageId { get; set; }
        public NameImages NameImage{ get; set; }
        public float Rating { get; set; }
    }
}
