using System.ComponentModel.DataAnnotations;
namespace CVHub.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Account
{
    public int Id { get; set; }
    public string? Photo { get; set; }
    // Для загрузки файла
    [NotMapped] 
    public IFormFile? PhotoFile { get; set; }

    [Required(ErrorMessage = "Enter your Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Enter your Surname")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Enter your Email")]
    [EmailAddress(ErrorMessage = "Enter a valid Email address")]
    public string Email { get; set; }

    public string Linkedin { get; set; }

    public string Github { get; set; }
    public string Age { get; set; }
    public string Role { get; set; }
    public string Seniority { get; set; }

    public string PlaceEarly { get; set; }
    public string Expirience { get; set; }

    public string? Skills { get; set; } // будет храниться в БД как строка
    [NotMapped]
    public string[] SelectedSkills { get; set; } = Array.Empty<string>();
}