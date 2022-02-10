using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto.NamesDTO
{
    public class CreateNamesDto
    {        
        public string ArabName { get; set; }
        public string OtherName { get; set; }
        public int DictionaryId { get; set; }
        public bool IsApproved { get; set; }
        public string UserId { get; set; }
    }
}
