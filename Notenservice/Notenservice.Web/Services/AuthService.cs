using System.Net.Http.Json;
namespace Web.Service;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly CustomAuthStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = (CustomAuthStateProvider)authStateProvider;
    }

    public async Task<AuthResponse?> RegisterAsync(UserRegistrationRequestDTO registerModel)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Authentication/register", registerModel);
        if (!response.IsSuccessStatusCode)
        {
            string error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Register failed ({(int)response.StatusCode} {response.ReasonPhrase}): {error}");
        }
        Console.WriteLine("Registration succesful!");
        return await response.Content.ReadFromJsonAsync<AuthResponse>();
    }

    public async Task<AuthResponse?> LoginAsync(UserLoginRequestDTO loginModel)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Authentication/login", loginModel);
        if (!response.IsSuccessStatusCode)
        {
            string error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Login failed ({(int)response.StatusCode} {response.ReasonPhrase}): {error}");
        }

        AuthResponse? authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();

        if (authResponse?.Token != null)
        {
            await _authStateProvider.MarkUserAsAuthenticated(authResponse.Token);
        }
        Console.WriteLine("Login succesful!");
        return authResponse;
    }
}


public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
}
public class UserLoginRequestDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
