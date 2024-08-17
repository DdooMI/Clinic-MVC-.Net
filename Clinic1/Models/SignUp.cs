using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Clinic1.Models
{
    public enum gender
    {
        male, female
    }
    public class SignUp
    {
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        [Key]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9.]+$", ErrorMessage = "Not Valid Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20)]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]       
        [Compare("Password", ErrorMessage = "Not Match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [RegularExpression(@"^01+[0-9]{9}$", ErrorMessage = "Must be 11 Digits ")]
        public string? Phone { get; set; }
        [Required]
        [Range(18,80)]
        public int? Age { get; set; }
        [Required]
       
        public gender gender { get; set; }

        [DefaultValue("flower.jpg")]
        public string? img { get; set; }

     
    }
}
