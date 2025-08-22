using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVHub.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int RegistrationId { get; set; }
        public Registration? Registration { get; set; }
        public string? Photo { get; set; }

        [NotMapped]
        public IFormFile? PhotoFile { get; set; }

        [Required(ErrorMessage = "Enter your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter your Email")]
        [EmailAddress(ErrorMessage = "Enter a valid Email address")]
        public string Email { get; set; }

        public string? Linkedin { get; set; }
        public string? Github { get; set; }
        public string? Age { get; set; }
        public string? Role { get; set; }
        public string? Seniority { get; set; }
        public string? PlaceEarly { get; set; }
        public string? Experience { get; set; }
        public string? Skills { get; set; }

        [NotMapped]
        public string[] SelectedSkills { get; set; } = Array.Empty<string>();

        
        public List<string> GetSkillsList()
        {
            return string.IsNullOrEmpty(Skills) 
                ? new List<string>() 
                : Skills.Split(',').ToList();
        }
    }
}