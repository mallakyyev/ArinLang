using System.ComponentModel.DataAnnotations;

namespace ARINLAB.Models
{
    public class CreateTestViewModel
    {
        [Display(Name = "Name"), Required(ErrorMessage = "Required")]
        public string Name { get; set; }
    }
}