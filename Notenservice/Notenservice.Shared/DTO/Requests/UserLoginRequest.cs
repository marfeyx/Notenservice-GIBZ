using System.ComponentModel.DataAnnotations;

namespace Todo.Shared.DTO;
public class UserLoginRequest
{
    [Required(ErrorMessage = "Der Benutzername ist ein Pflichtfeld")]
    [MaxLength(50, ErrorMessage = "Der Benutzername darf maximal 50 Zeichen lang sein.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Das Passwort ist ein Pflichtfeld")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$",
    ErrorMessage = "Das Passwort erfüllt nicht alle Anforderungen (mind. 6 Zeichen, mit Grossbuchstabe, Ziffer und Sonderzeichen).")]
    public string Password { get; set; } = string.Empty;

    public bool Success { get; set; }
    public string[] Errors { get; set; } = [];

    public string Token { get; set; } = string.Empty;
}