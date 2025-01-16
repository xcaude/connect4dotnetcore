namespace Connect4.Entities
{
    /// <summary>
    /// Represents a genre in the Connect 4 game.
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Gets or sets the genre id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the genre name.
        /// </summary>
        public required string Name { get; set; }
    }
}