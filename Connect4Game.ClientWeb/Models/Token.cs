using Connect4Game.ClientWeb.Models;


namespace Connect4Game.ClientWeb.Models
{
    /// <summary>
    /// Represents a token in the connect 4 game.
    /// </summary>
    public class Token
    {

        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the color of the token.
        /// </summary>
        public string Color { get; set; } = "Yellow";
    }
}