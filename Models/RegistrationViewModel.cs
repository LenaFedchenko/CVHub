using System.ComponentModel.DataAnnotations;
namespace CVHub.Models;

public class Registration
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Enter your Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Enter your Surname")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Enter your Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Enter Password")]
    [StringLength(8, ErrorMessage ="Your password must have 8 symbols")]
    public string Password { get; set; }
}