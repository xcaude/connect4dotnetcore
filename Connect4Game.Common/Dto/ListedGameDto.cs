namespace Connect4Game.Common.Dto
{
    public class ListedGameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Host { get; set; }
        public string? Guest { get; set; }
    }
}