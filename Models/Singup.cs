using System.ComponentModel.DataAnnotations;

namespace Interim.Models
{
    public class Singup
    {
        [Key]
        
        public int UserId { get; set; }

        [Required(ErrorMessage ="Please Enter Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
    }
}
