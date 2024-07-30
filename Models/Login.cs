using System.ComponentModel.DataAnnotations;
namespace Interim.Models
{
    public class Login
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

        

    }
}
