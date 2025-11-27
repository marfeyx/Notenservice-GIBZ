using Shared.DTOs;

namespace Web.Service;
public interface IAuthService
{
    Task<AuthResponse?> RegisterAsync(RegisterRequestDTO registerModel);
    Task<AuthResponse?> LoginAsync(UserLoginRequestDTO loginModel);
}