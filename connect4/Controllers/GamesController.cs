using Connect4.Data;
using Microsoft.AspNetCore.Mvc;
using Connect4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


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

    // list all avainlable games and games in progress concerning the player
    [HttpGet]
    public async Task<IActionResult> ListAvailableGames()
        {
            var games = await _context.Games
                .Where(g => g.Status == "Awaiting Guest")
                .Select(g => new { g.Id, g.Name, g.Host })
                .ToListAsync();

            return Ok(games);
        }

// 4. List My Games
    [HttpGet("my")]
    public async Task<IActionResult> ListMyGames()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Players.FindAsync(userId);
            if (user == null)
                return Unauthorized();

            var games = await _context.Games
                .Where(g => g.Host == user || g.Guest == user)
                .Select(g => new
                {
                    g.Id,
                    g.Status,
                    Opponent = g.Host == user ? g.Guest : g.Host,
                    YourTurn = g.CurrentTurn == user
                })
                .ToListAsync();

            return Ok(games);
        }    
    
    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Extract user ID from JWT
        var host = await _context.Players.FindAsync(userId);
        if (host == null) return NotFound("Host not found");

        var game = new Game
        {
            Host = host,
            HostId = userId!,
            Name = dto.Name,
            Status = "Awaiting Guest",
            CurrentTurnId = userId!,
            Grid = new Grid()
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        return Ok(game);
    }

    [HttpPost("{gameId}/join")]
    public async Task<IActionResult> JoinGame(int gameId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var user = await _context.Players.FindAsync(userId);
            if (user == null)
                return Unauthorized();

            var game = await _context.Games.FindAsync(gameId);
            if (game == null)
                return NotFound();

            if (game.Status != "Awaiting Guest")
                return BadRequest("Game is not available to join.");

            game.Guest = user;
            game.GuestId = userId!;
            game.Status = "In Progress";
            await _context.SaveChangesAsync();

            return Ok(game);
        }
    [HttpPost("{gameId}/play")]
    public async Task<IActionResult> PlayTurn(int gameId, [FromBody] PlayTurnRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Players.FindAsync(userId);
            if (user == null)
                return Unauthorized();

            var game = await _context.Games.Include(g => g.Grid).FirstOrDefaultAsync(g => g.Id == gameId);
            if (game == null)
                return NotFound();

            if (game.Status != "In Progress")
                return BadRequest("Game is not in progress.");

            if (game.CurrentTurn != user)
                return Forbid("It is not your turn.");
            Token token = new Token();
            token.Color = user == game.Host ? "Red" : "Yellow";
            if (!game.Grid.DropToken(request.Column, token))
                return BadRequest("Invalid move. Column may be full or out of range.");

            // Check for win or draw
            if (game.Grid.CheckWinCondition())
            {
                game.Status = "Finished";
                game.Winner = game.Guest == user ? game.Guest : game.Host;
            }
            else if (game.Grid.IsFull())
            {
                game.Status = "Finished";
                game.Winner = null; 
            }
            else
            {
                game.CurrentTurn = game.Host == user ? game.Guest : game.Host;
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                game.Id,
                game.Grid,
                game.Status,
                game.Winner
            });
        }

        // 6. Get Game Details
        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGameDetails(int gameId)
        {
            var game = await _context.Games.Include(g => g.Grid).FirstOrDefaultAsync(g => g.Id == gameId);
            if (game == null)
                return NotFound();

            return Ok(new
            {
                game.Id,
                game.Host,
                game.Guest,
                game.Status,
                game.Grid,
                game.CurrentTurn,
                game.Winner
            });
        }
}