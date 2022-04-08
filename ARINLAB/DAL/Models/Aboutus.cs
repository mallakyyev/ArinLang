using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Aboutus
    {
        [Key]
        public int Id { get; set; }
        public string TittleTM { get; set; }
        public string DescriptionTM { get; set; }

        public string TittleRU { get; set; }
        public string DescriptionRU { get; set; }

        public string TittleEN { get; set; }
        public string DescriptionEN { get; set; }

    }
}
