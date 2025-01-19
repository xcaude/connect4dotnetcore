namespace Connect4Game.Domain.Commands
{
    public class CreatePlayerCommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}