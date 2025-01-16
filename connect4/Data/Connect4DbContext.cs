using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Connect4.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Connect4.Data
{
    public class Connect4DbContext : IdentityDbContext<IdentityUser>
    {
        public Connect4DbContext(DbContextOptions<Connect4DbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<Token> Tokens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Host)
                .WithMany()  // No need to map 'Games' for Host
                .HasForeignKey(g => g.HostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Guest)
                .WithMany()  // No need to map 'Games' for Guest
                .HasForeignKey(g => g.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.CurrentTurn)
                .WithMany()  // No need to map 'Games' for CurrentTurn
                .HasForeignKey(g => g.CurrentTurnId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grid>()
                .HasMany(g => g.Cells)
                .WithOne(c => c.Grid)
                .HasForeignKey(c => c.GridId)
                .OnDelete(DeleteBehavior.Cascade);  

            

        }

    }
}
