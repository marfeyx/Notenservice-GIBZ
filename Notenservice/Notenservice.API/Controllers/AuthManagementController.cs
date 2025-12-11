using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Todo.Shared.DTO;

[Route("api/[controller]")]
[ApiController]
public class AuthManagementController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtConfig _jwtConfig;

    public AuthManagementController(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor)
    {
        _userManager = userManager;
        _jwtConfig = optionsMonitor.CurrentValue;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(UserRegistrationRequestDTO user)
    {
        // Check if the incoming request is valid
        if (ModelState.IsValid)
        {
            // check i the user with the same email exist
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser != null)
            {
                return BadRequest(new UserRegistrationRequestDTO()
                {
                    Success = false,
                    Errors = ["E-Mail-Adresse existiert bereits."]
                });
            }

            var newUser = new IdentityUser() { Email = user.Email, UserName = user.Username };
            var isCreated = await _userManager.CreateAsync(newUser, user.Password);
            if (isCreated.Succeeded)
            {
                var jwtToken = GenerateJwtToken(newUser);

                return Ok(new RegistrationResponse()
                {
                    Success = true,
                    Token = jwtToken
                });
            }

            return new JsonResult(new RegistrationResponse()
            {
                Success = false,
                Errors = isCreated.Errors.Select(x => x.Description).ToList()
            }
                    )
            { StatusCode = 500 };
        }

        return BadRequest(new RegistrationResponse()
        {
            Success = false,
            Errors = new List<string>(){
                                            "Ungültiger Payload"
                                        }
        });
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(UserLoginRequest user)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser == null)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Success = false,
                    Errors = new List<string>(){
                                            "Ungültige Authentifizierungsanfrage"
                                        }
                });
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

            if (isCorrect)
            {
                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new RegistrationResponse()
                {
                    Success = true,
                    Token = jwtToken
                });
            }
            else
            {
                // We dont want to give to much information on why the request has failed for security reasons
                return BadRequest(new RegistrationResponse()
                {
                    Success = false,
                    Errors = new List<string>(){
                                            "Ungültige Authentifizierungsanfrage"
                                        }
                });
            }
        }

        return BadRequest(new RegistrationResponse()
        {
            Success = false,
            Errors = new List<string>(){
                                            "Ungültiger Payload"
                                        }
        });
    }
}