namespace Connect4.Entities;

public class Game
{
    /// <summary>
    /// Gets or sets the game id.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the game name.
    /// </summary>
    public required string  Name { get; set; }
    /// <summary>
    /// get the genre id.
    /// </summary>
    public int GenreId { get; set; }
    /// <summary>
    /// Gets or sets the game genre.
    /// </summary>
    public Genre? Genre { get; set; }
    /// <summary>
    /// Gets or sets the game price.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Gets or sets the game release date.
    /// </summary>
    public DateTime ReleaseDate { get; set; }
}