using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Configs
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Name_ar { get; set; }
        public int CountryCode { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
