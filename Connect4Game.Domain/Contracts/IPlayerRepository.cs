using Connect4Game.Domain.Model;

namespace Connect4Game.Domain.Contracts
{
    public interface IPlayerRepository
    {
        Task<Player?> GetByIdAsync(string id);
        Task<IEnumerable<Player>> GetAllAsync();
        Task AddAsync(Player player);
        Task UpdateAsync(Player player);
        Task DeleteAsync(string id);
    }
}