using Connect4.Data;
using Microsoft.AspNetCore.Mvc;
using Connect4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GamesController : ControllerBase
{
    private readonly Connect4DbContext _context;

    public GamesController(Connect4DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetGames()
    {
        Console.WriteLine("GetGames");
        var games = await _context.Games.ToListAsync();
        return Ok(games);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameDto dto)
    {
        var host = await _context.Players.FindAsync(dto.HostId);
        if (host == null) return NotFound("Host not found");

        var game = new Game
        {
            Host = host,
            HostId = dto.HostId,
            Status = "Awaiting Guest",
            CurrentTurnId = dto.HostId,
            Grid = new Grid()
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        return Ok(game);
    }

    [HttpPost("{id}/join")]
    public async Task<IActionResult> JoinGame(int id, [FromBody] JoinGameDto dto)
    {
        var game = await _context.Games.FindAsync(id);
        if (game == null) return NotFound("Game not found");

        var guest = await _context.Players.FindAsync(dto.GuestId);
        if (guest == null) return NotFound("Guest not found");
        game.Guest = guest;
        game.GuestId = dto.GuestId;
        game.Status = "In Progress";

        await _context.SaveChangesAsync();
        return Ok(game);
    }
}
