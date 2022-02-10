using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class WordClauseDto
    {
        public int Id { get; set; }        
        public string ArabClause { get; set; }
        public string OtherClause { get; set; }
        public string ArabReader { get; set; }
        public string OtherReader { get; set; }        
        public bool IsApproved { get; set; }        
        public string UserName { get; set; }
        public string DictionaryName { get; set; }
        public string CategoryName { get; set; }
        


    }
}
