using Microsoft.EntityFrameworkCore;
using Connect4Game.Models;

namespace Connect4Game.Api
{
    public class Connect4DbContext : DbContext
    {
        public Connect4DbContext(DbContextOptions<Connect4DbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}