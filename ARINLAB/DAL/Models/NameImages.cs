using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class NameImages
    {
        public int Id { get; set; }
        public string ImageUri { get; set; }
        public int NamesId { get; set; }
        public bool IsApproved { get; set; }
        public string UserId { get; set; }
        public Names Names { get; set; }
        public ICollection<NamesImageRating> NamesImageRatings { get; set; }
    }
}
