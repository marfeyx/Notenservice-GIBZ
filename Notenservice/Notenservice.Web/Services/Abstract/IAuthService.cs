namespace Web.Service;
public interface IAuthService
{
    Task<AuthResponse?> RegisterAsync(UserRegistrationRequestDTO registerModel);
    Task<AuthResponse?> LoginAsync(UserLoginRequestDTO loginModel);
}
