using DAL.Enums;
using DAL.Models.Configs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class EditUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int  Role{ get; set; }
        public string Accupation { get; set; }
        
        
    }
}
