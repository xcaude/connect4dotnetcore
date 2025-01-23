using Connect4Game.Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Connect4Game.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Connect4Game.Infrastructure.Context;



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
                .Where(g => g.Status == "Awaiting Guest" || g.Status == "In Progress" && (g.HostId == User.FindFirstValue(ClaimTypes.NameIdentifier) || g.GuestId == User.FindFirstValue(ClaimTypes.NameIdentifier)))
                .Select(g => new ListedGameDto
                { 
                    Id = g.Id,
                    Status = g.Status,
                    Name = g.Name,
                    Host = g.Host.UserName!,
                    Guest = g.Guest != null ? g.Guest.UserName : "Awaiting Guest"
                })
                .ToListAsync();

            return Ok(games);
        }
    
    // This Endpoint wont be useful
    /* 
    [HttpGet("my")]
    public async Task<IActionResult> ListMyGames()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Players.FindAsync(userId);
            if (user == null)
                return Unauthorized();
            // contains the games where the user is host or guest or that are awaiting guest
            var games = await _context.Games
            .Where(g => g.HostId == userId || g.GuestId == userId || g.Status == "Awaiting Guest")
            .Select(g => new ListedGameDto
            {
                Id = g.Id,
                Status = g.Status,
                Name = g.Name,
                Host = g.Host.UserName!,
                Guest = g.Guest != null ? g.Guest.UserName : "Awaiting Guest"
            })
            .ToListAsync();
            return Ok(games);
        }    
    */
    
    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
        Console.WriteLine(userId);
        var host = await _context.Players.FindAsync(userId);
        if (host == null) return NotFound("Host not found");

        var game = new Game
        {
            Host = host,
            HostId = userId!,
            Name = dto.Name,
            Status = "Awaiting Guest",
            CurrentTurnId = userId!,
            CurrentTurn = host,
            Grid = new string('0', 6 * 7)
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("{gameId}/join")]
    public async Task<IActionResult> JoinGame(int gameId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var user = await _context.Players.FindAsync(userId);
            if (user == null)
                return Unauthorized();

            /*var game = await _context.Games.FindAsync(gameId);
            if (game == null)
                return NotFound();*/

            var game = await _context.Games
                .Include(g => g.Host)
                .Include(g => g.Guest)
                .Include(g => g.CurrentTurn)
                .FirstOrDefaultAsync(g => g.Id == gameId);
            if (game == null)
                return NotFound();

            if (game.Status != "Awaiting Guest")
                return BadRequest("Game is not available to join.");

            Console.WriteLine("USER qui join : " + user);
            game.Guest = user;
            game.GuestId = userId!;
            game.Status = "In Progress";
            //game.JoinGame(user);
            _context.Entry(game).State = EntityState.Modified;
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

            var game = await _context.Games
                .Include(g => g.Host)
                .Include(g => g.Guest)
                .Include(g => g.CurrentTurn)
                .FirstOrDefaultAsync(g => g.Id == gameId);
            if (game == null)
                return NotFound();

            if (game.Status != "In Progress")
                return BadRequest("Game is not in progress.");

            Console.WriteLine("USER qui play: " + user);

            if (game.CurrentTurn != user)
                return Forbid("It is not your turn.");
            if(game.PlayTurn(user, request.Column)){
                game.CurrentTurnId = game.CurrentTurn?.Id;
                _context.Entry(game).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new GridDto {
                Grid = game.Grid
                });
            }
            return Ok("Invalid move");
        }

        // 6. Get Game Details
        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGameDetails(int gameId)
        {
            var game = await _context.Games
                .Include(g => g.Host)
                .Include(g => g.Guest)
                .FirstOrDefaultAsync(g => g.Id == gameId);
            if (game == null)
                return NotFound();

            return Ok(game);
        }
}