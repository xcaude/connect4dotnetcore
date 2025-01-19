using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Connect4Game.Domain.Models;

namespace Connect4Game.Infrastructure.Models
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name).IsRequired().HasMaxLength(100);
            builder.Property(g => g.Grid).IsRequired();
            builder.Property(g => g.Status).IsRequired().HasMaxLength(20);

            builder.HasOne(g => g.Host)
                   .WithMany(p => p.Games)
                   .HasForeignKey(g => g.HostId)
                   .OnDelete(DeleteBehavior.Restrict);

            // builder.HasOne(g => g.Guest)
            //        .WithMany()
            //        .HasForeignKey(g => g.GuestId)
            //        .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(g => g.CurrentTurn)
                   .WithMany()
                   .HasForeignKey(g => g.CurrentTurnId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}