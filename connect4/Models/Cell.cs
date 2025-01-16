namespace Connect4.Models
{
    /// <summary>
    /// Represents a cell in the Connect 4 grid.
    /// </summary>
    public class Cell
    {   
        public int Id { get; set; }  // Primary key for the Cell
        public int Row { get; set; }
        public int Column { get; set; }
        public int GridId { get; set; }  // Foreign key to the Grid

        public Token? Token { get; set; }

        public required Grid Grid { get; set; } 
    }
}