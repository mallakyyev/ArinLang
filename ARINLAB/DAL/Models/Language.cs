using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Language
    {
        public string Culture { get; set; }

        public string Name { get; set; }

        public bool IsPublish { get; set; }

        public string FlagImage { get; set; }
        public int DisplayOrder { get; set; }
    }
}
