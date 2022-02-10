using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Areas.Admin.Models
{
    public class EditUserModel
    {           
        public string Id { get; set; }
        public Gender Gender { get; set; }
        public int CountryId { get; set; }
        public bool IsApproved { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string FamilyName { get; set; }               

        public string Accupation { get; set; }
    }
}
