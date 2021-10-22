using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarPartsPatron.Auth.Models
{
    public class Registration
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DisplayName("DisplayName")]
        public string DisplayName { get; set; }

        [Required]
        [DisplayName("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("LastName")]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }
 
        [Required]
        [Compare(nameof(Password))]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
