using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly JwtService _jwtService;

    public AuthController(UserRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var existing = await _userRepository.GetByUsernameAsync(dto.Username);
        if (existing is not null)
            return BadRequest(new { message = "Username already taken." });

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = await _userRepository.CreateAsync(dto.Username, passwordHash);

        return Ok(new AuthResponseDto
        {
            UserId = user.Id,
            Username = user.Username,
            Token = _jwtService.GenerateToken(user)
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "Invalid username or password." });

        return Ok(new AuthResponseDto
        {
            UserId = user.Id,
            Username = user.Username,
            Token = _jwtService.GenerateToken(user)
        });
    }
}
