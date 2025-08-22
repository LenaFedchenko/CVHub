using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CVHub.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Registration
    {
        public int Id { get; set; }
        
        // Навигационное свойство
        public virtual Account? Account { get; set; }

        [Required(ErrorMessage = "Enter your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter your Email")]
        [EmailAddress(ErrorMessage = "Enter a valid Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [MinLength(8, ErrorMessage = "Your password must be at least 8 characters")]
        public string Password { get; set; }
    }
}