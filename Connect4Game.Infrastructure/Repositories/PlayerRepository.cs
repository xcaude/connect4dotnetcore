using Connect4Game.Domain.Contracts;
using Connect4Game.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Connect4Game.Infrastructure.Context;

namespace Connect4Game.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly Connect4DbContext _context;

        public PlayerRepository(Connect4DbContext context)
        {
            _context = context;
        }

        public async Task<Player?> GetByIdAsync(string id)
        {
            return await _context.Players.FindAsync(id);
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task AddAsync(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
        }
    }
}