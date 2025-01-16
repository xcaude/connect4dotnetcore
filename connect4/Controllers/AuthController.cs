

using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly Connect4DbContext _context;
    private readonly UserManager<Player> _userManager;
    private readonly SignInManager<Player> _signInManager;
    private readonly IConfiguration _configuration;

    public PlayersController(Connect4DbContext context, 
                             UserManager<Player> userManager, 
                             SignInManager<Player> signInManager, 
                             IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlayers()
    {
        var players = await _context.Players.ToListAsync();
        return Ok(players);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var player = new Player
        {
            UserName = registerDto.Login,
        };
        var result = await _userManager.CreateAsync(player, registerDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
    {   
        var player = await _userManager.FindByNameAsync(loginDto.Login);
        if (player == null || !await _userManager.CheckPasswordAsync(player, loginDto.Password))
        {
            return Unauthorized("Invalid login or password");
        }
        
        var token = GenerateJwtToken(player);
        return Ok(new { Token = token });
    }
   
    private string GenerateJwtToken(Player user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub , user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(10),
            signingCredentials: signin
        );
        string tokenvalue = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        Console.WriteLine(BitConverter.ToString(Encoding.UTF8.GetBytes(tokenvalue)));
        Console.WriteLine($"Generated Token: {tokenvalue}");

        return tokenvalue;
    }


}
