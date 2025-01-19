namespace Connect4Game.Domain.Commands
{
    public class CreateGameCommand
    {
        public string Name { get; set; }
        public string HostId { get; set; }
    }
}