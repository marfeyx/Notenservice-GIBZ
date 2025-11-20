namespace API.DTOs.Authentication;

/// <summary>
/// Data Transfer Object for login requests.
/// </summary>
public class LoginRequestDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
