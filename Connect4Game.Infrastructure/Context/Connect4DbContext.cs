using Microsoft.EntityFrameworkCore;
using Connect4Game.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Connect4Game.Infrastructure.Context
{
    public class Connect4DbContext : IdentityDbContext<Player>
    {
        public Connect4DbContext(DbContextOptions<Connect4DbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Connect4DbContext).Assembly);
        }

    }
}
