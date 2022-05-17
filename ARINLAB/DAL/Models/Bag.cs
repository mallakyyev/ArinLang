using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Bag
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Problem { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }
}
