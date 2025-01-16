using Microsoft.AspNetCore.Authorization.Infrastructure;

using Connect4Game.ClientWeb.Models;

namespace Connect4Game.ClientWeb.Models
{
    /// <summary>
    /// Represents a game of Connect 4.
    /// </summary>
    public class Game
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the host of the game.
        /// </summary>
        public Player Host { get; set; }  = new Player();
        public required string HostId { get; set; }
        /// <summary>
        /// Gets or sets the guest of the game.
        /// </summary>
        public Player? Guest { get; set; }
        public string GuestId { get; set; }

        public required string Name { get; set; }
        /// <summary>
        /// Gets or sets the grid of the game.
        /// </summary>
        public Grid Grid { get; set; } = new Grid();
        /// <summary>
        /// Gets or sets the status of the game.
        /// </summary>
        public string Status { get; set; } = "Awaiting Guest";

        public required string CurrentTurnId { get; set; }
        /// <summary>
        /// Gets or sets the current turn of the game.
        /// </summary>
        public Player? CurrentTurn { get; set; } 

        public Player? Winner { get; set; }

        /// <summary>
        /// Starts the game and initializes the first player to guest.
        /// </summary>
        public void StartGame()
        {
            if (Guest != null)
            {
                Status = "In Progress";
                CurrentTurn = Guest;
            }
        }
        /// <summary>
        /// Joins a game as a guest and starts the game.
        /// </summary>
        /// <param name="guest">The guest player</param>
        public void JoinGame(Player guest)
        {
            if (Guest == null)
            {
                Guest = guest;
                StartGame();
            }
        }

        /// <summary>
        /// Plays a turn in the game by calling the DropToken method on the grid.
        /// </summary>
        /// <param name="player">The player that drops the token</param>
        /// <param name="column">The column in which the column will be droped </param>
        public void PlayTurn(Player player, int column)
        {
            if (Status == "In Progress" && player == CurrentTurn)
            {
                var token = new Token { Color = player == Host ? "Red" : "Yellow" };
                Grid.DropToken(column, token);
                if (CheckWinCondition())
                {
                    Status = "Finished";
                }
                else if (Grid.IsFull())
                {
                    Status = "Finished";
                }
                else
                {
                    CurrentTurn = CurrentTurn == Host ? Guest : Host;
                }
            }
        }
        /// <summary>
        /// Checks the win condition of the game by calling the CheckWinCondition method on the grid.
        /// </summary>
        /// <returns>True if won else false</returns>
        public bool CheckWinCondition()
        {
            return Grid.CheckWinCondition();
        }
    }
}