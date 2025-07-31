using System.ComponentModel.DataAnnotations;
namespace CVHub.Models;

public class Authorisation
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Enter your Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Enter Password")]
    [StringLength(8, ErrorMessage ="Your password must have 8 symbols")]
    public string Password { get; set; }
}