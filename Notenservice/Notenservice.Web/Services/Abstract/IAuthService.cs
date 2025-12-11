namespace Web.Service;
public interface IAuthService
{
    Task<AuthResponse?> RegisterAsync(UserLoginRequestDTO registerModel);
    Task<AuthResponse?> LoginAsync(UserLoginRequestDTO loginModel);
}