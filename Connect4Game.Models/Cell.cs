namespace Connect4Game.Models
{
    /// <summary>
    /// Represents a cell in the Connect 4 grid.
    /// </summary>
    public class Cell
    {   
        /// <summary>
        /// Gets or sets the row index of the cell.
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// Gets or sets the column index of the cell.
        /// </summary>
        public int Column { get; set; }
        /// <summary>
        /// Gets or sets the token in the cell.
        /// </summary>
        public Token? Token { get; set; }
    }
}