using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace Clinic1.Models
{
    public class Reservation
    {
        [Key]
        [ForeignKey("LogIn")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public TimeSpan time { get; set; }
        [Required]
        public string Special { get; set; }
        
        [Required]
        [RegularExpression(@"^01+[0-9]{9}$", ErrorMessage = "Must be 11 Digits ")]
        public string? Phone { get; set; }       


    }
}
