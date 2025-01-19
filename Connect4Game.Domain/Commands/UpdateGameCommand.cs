namespace Connect4Game.Domain.Commands
{
    public class UpdateGameCommand
    {
        public int Id { get; set; }
        public string Grid { get; set; }
        public string Status { get; set; }
        public string CurrentTurnId { get; set; }
        public string? WinnerId { get; set; }
    }
}