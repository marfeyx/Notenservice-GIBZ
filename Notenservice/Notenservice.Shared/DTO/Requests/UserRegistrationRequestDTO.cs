using System.ComponentModel.DataAnnotations;

namespace Todo.Shared.DTO;
public class UserRegistrationRequestDTO
{
    [Required(ErrorMessage = "Der Benutzername ist ein Pflichtfeld")]
    [MaxLength(50, ErrorMessage = "Der Benutzername darf maximal 50 Zeichen lang sein.")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Die Email ist ein Pflichtfeld")]
    [EmailAddress]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Das Passwort ist ein Pflichtfeld")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$",
   ErrorMessage = "Das Passwort erfüllt nicht alle Anforderungen (mind. 6 Zeichen, mit Grossbuchstabe, Ziffer und Sonderzeichen).")]
    public required string Password { get; set; }

    public bool Success { get; set; }
    public string[] Errors { get; set; } = [];

    public string Token { get; set; } = string.Empty;
}