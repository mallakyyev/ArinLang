using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Dto
{
    public class EditWordClauseDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string ArabClause { get; set; }
        [Required]
        public string OtherClause { get; set; }
        [Required]
        public string ArabReader { get; set; }
        [Required]
        public string OtherReader { get; set; }
        [Required]
        public int DictionaryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsApproved { get; set; }
        public int? Viewed { get; set; }
        public string UserId { get; set; }
       
    }
}
