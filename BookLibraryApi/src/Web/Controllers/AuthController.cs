using AutoMapper;
using BookLibraryApi.src.Application.Abstractions.Services;
using BookLibraryApi.src.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryApi.src.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AuthController(IUserService userService, ITokenService tokenService, IMapper mapper)
    {
        _userService = userService;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [AllowAnonymous]
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

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
    {
        var user = await _userService.Register(request.Username, request.Password, ct);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, _mapper.Map<UserResponse?>(user));
    }

    [AllowAnonymous]
    [HttpGet("{id::guid}")]
    public async Task<ActionResult<UserResponse?>> Get(Guid id, CancellationToken ct)
    {
        var user = await _userService.GetAsync(id, ct);
        return Ok(_mapper.Map<UserResponse?>(user));
    }
}
