using Microsoft.AspNetCore.Identity;
namespace Connect4.Models
{
    /// <summary>
    /// Represents a player in the Connect 4 game.
    /// </summary>
    public class Player : IdentityUser
    {
        /// <summary>
        /// Gets or sets the login of the player.
        /// </summary>
        public string Login { get; set; } = "player";
        /// <summary>
        /// Gets or sets the password of the player.
        /// </summary>
        public string Password { get; set; } = "password";
    
        /// <summary>
        /// Gets or sets the list of wins the player has.
        /// </summary>
        /// <param name="login">The username of the player trying to authenticate</param>
        /// <param name="password">The password of the player trying to authenticate</param>
        /// <returns>true if the authentication suceed else false</returns>
        public bool Authenticate(string login, string password)
        {
            return Login == login && Password == password;
        }
    }
}