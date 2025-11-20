namespace Shared.DTOs;

/// <summary>
/// Data Transfer Object for registration requests.
/// </summary>
public class RegisterRequestDTO
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
