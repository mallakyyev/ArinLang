using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Names
    {
        public int Id { get; set; }
        public string ArabName { get; set; }
        public string OtherName { get; set; }
        public int DictionaryId { get; set; }
        public bool IsApproved { get; set; }
        public string UserId { get; set; }
        public string ImageForShare { get; set; }
        public Dictionary Dictionary{ get; set; }
        public ICollection<NameImages> NameImages { get; set; }
        public ICollection<NamesRating> NamesRatings { get; set; }

    }
}
