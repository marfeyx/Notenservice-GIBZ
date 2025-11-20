using API.DTOs.Authentication;
using API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthenticationController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
    {
        if (request == null) return BadRequest("Request body is null");

        string token = await _userService.RegisterAsync(request);
        return Ok(new { Token = token });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDTO request)
    {
        try
        {
            string token = await _userService.LoginAsync(request);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
