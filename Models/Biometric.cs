using System.ComponentModel.DataAnnotations;
namespace Interim.Models
{
    public class Biometric
    {
        [Key]
        public int RegistrationID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        
        public int Height { get; set; }
        [Required]

        public int Weight { get; set; }
        [Required]

        public string BloodGroup { get; set; }


        public string Disease { get; set; }
    }
}
