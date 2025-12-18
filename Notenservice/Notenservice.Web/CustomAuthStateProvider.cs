using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime js;
    private const string TokenKey = "authToken";

    public CustomAuthStateProvider(IJSRuntime js)
    {
        this.js = js;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string token = await js.InvokeAsync<string>("localStorage.getItem", TokenKey);

        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        ClaimsIdentity identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwtAuth");
        ClaimsPrincipal user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        await js.InvokeVoidAsync("localStorage.setItem", TokenKey, token);

        ClaimsIdentity identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwtAuth");
        ClaimsPrincipal user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        if (string.IsNullOrWhiteSpace(jwt)) yield break;

        string[] parts = jwt.Split('.');
        if (parts.Length < 2) yield break;

        string payload = parts[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);

        using JsonDocument doc = JsonDocument.Parse(jsonBytes);
        JsonElement root = doc.RootElement;

        foreach (JsonProperty prop in root.EnumerateObject())
        {
            string name = prop.Name;
            JsonElement value = prop.Value;

            if (string.Equals(name, "role", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(name, "roles", StringComparison.OrdinalIgnoreCase))
            {
                if (value.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement item in value.EnumerateArray())
                    {
                        string? role = item.GetString();
                        if (!string.IsNullOrEmpty(role))
                            yield return new Claim(ClaimTypes.Role, role);
                    }
                }
                else if (value.ValueKind == JsonValueKind.String)
                {
                    string? roleStr = value.GetString();
                    if (!string.IsNullOrEmpty(roleStr))
                    {
                        string[] roles = roleStr.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string r in roles)
                            yield return new Claim(ClaimTypes.Role, r);
                    }
                }

                continue;
            }

            if (value.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement item in value.EnumerateArray())
                {
                    yield return new Claim(name, item.ToString() ?? string.Empty);
                }
            }
            else
            {
                yield return new Claim(name, value.ToString() ?? string.Empty);
            }
        }
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        base64 = base64.Replace('-', '+').Replace('_', '/');
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
