using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic1.Models
{
    public class LogIn
    {
        [Key]       
        [Required]
        [Display(Name = "Email Address")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9.]+$", ErrorMessage = "Not Valid Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter your Password first")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
