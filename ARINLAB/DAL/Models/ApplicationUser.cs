using DAL.Enums;
using DAL.Models.Configs;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public Gender Gender { get; set; }        
        public int CountryId { get; set; }
        public bool IsApproved { get; set; }
        public string Accupation { get; set; }
        public Country Country { get; set; }
        public ICollection<WordSentences> WordSentences { get; set; }
        public ICollection<Word> Words { get; set; }
        public ICollection<WordClause> WordClauses { get; set; }
        public ICollection<Names> Names { get; set; }
    }
}
