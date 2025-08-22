using System.ComponentModel.DataAnnotations;

namespace CVHub.Models
{
    public class Authorisation
    {
        [Required(ErrorMessage = "Enter your Email")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }
    }
}