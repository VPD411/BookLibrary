using BookLibraryApi.src.Application.Abstractions.Services;
using BookLibraryApi.src.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryApi.src.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var user = await _userService.Authenticate(request.Username, request.Password, ct);
        if (user is null)
        {
            return Unauthorized(new { message = "Invalid username or login." });
        }

        var token = _tokenService.GenerateToken(user);
        return Ok(new { token, username = user.Username, role = user.Roles });
    }
}
