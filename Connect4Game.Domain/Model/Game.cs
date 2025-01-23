
namespace Connect4Game.Domain.Model
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
        public string Grid { get; set; } = InitializeGrid();
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

        private const int Rows = 6;
        private const int Columns = 7;

        private static string InitializeGrid()
        {
            return new string('0', Rows * Columns); // '0' represents an empty cell
        }
        /// <summary>
        /// Starts the game and initializes the first player to guest.
        /// </summary>
        public void StartGame()
        {
            Console.WriteLine("GAME IS STARTED");
            if (Guest != null)
            {
                Status = "In Progress";
                CurrentTurn = Guest;
                CurrentTurnId = Guest.Id;
                Console.WriteLine("Current turn after start game : " + CurrentTurn);
                Console.WriteLine("Current turn id after start game : " + CurrentTurnId);
            }
        }
        /// <summary>
        /// Joins a game as a guest and starts the game.
        /// </summary>
        /// <param name="guest">The guest player</param>
        public void JoinGame(Player guest)
        {
            Console.WriteLine("GAME IS JOINED");
            if (Guest == null)
            {
                
                Guest = guest;
                GuestId = guest.Id;
                Console.WriteLine("Guest : " + Guest);
                Console.WriteLine("GuestId : " + GuestId);
                StartGame();
            }
        }
        public bool PlayTurn(Player player, int column)
        {

            Console.WriteLine("PLAY TURN");
            if (column < 0 || column >= Columns)
            {
                return false;
            }

            if (Status == "In Progress" && player == CurrentTurn)
            {
                char tokenColor = player == Host ? 'R' : 'Y';
                DropToken(column, tokenColor);
                if (CheckWinCondition(tokenColor))
                {
                    Status = "Finished";
                    Winner = player;
                }
                else if (IsGridFull())
                {
                    Status = "Finished";
                }
                else
                {
                    Console.WriteLine("Host : " + Host);
                    Console.WriteLine("Guest : " + Guest);
                    CurrentTurn = CurrentTurn == Host ? Guest : Host;
                    CurrentTurnId = CurrentTurn?.Id;
                    Console.WriteLine("CurrentTurnId : " + CurrentTurnId);
                    Console.WriteLine("CurrentTurn : " + CurrentTurn);
                }
                return true;
            }
            return false;

        }
        private void DropToken(int column, char tokenColor)
        {

            Console.WriteLine("Column : " + column);
            Console.WriteLine("TokenColor : " + tokenColor);

            if (column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column), "Column index is out of range.");
            }

            for (int row = Rows - 1; row >= 0; row--)
            {
                int index = row * Columns + column;
                if (index < 0 || index >= Grid.Length)
                {
                    throw new IndexOutOfRangeException("Calculated index is out of range.");
                }

                if (Grid[index] == '0')
                {
                    var gridArray = Grid.ToCharArray();
                    gridArray[index] = tokenColor;
                    Grid = new string(gridArray);
                    break;
                }
            }
        }

        private bool CheckWinCondition(char tokenColor)
        {
            // Check horizontal, vertical, and diagonal win conditions
            return CheckHorizontalWin(tokenColor) || CheckVerticalWin(tokenColor) || CheckDiagonalWin(tokenColor);
        }
        private bool CheckHorizontalWin(char tokenColor)
        {
            for (int row = 0; row < Rows; row++)
            {
                int count = 0;
                for (int col = 0; col < Columns; col++)
                {
                    int index = row * Columns + col;
                    if (Grid[index] == tokenColor)
                    {
                        count++;
                        if (count == 4) return true;
                    }
                    else
                    {
                        count = 0;
                    }
                }
            }
            return false;
        }
        private bool CheckVerticalWin(char tokenColor)
        {
            for (int col = 0; col < Columns; col++)
            {
                int count = 0;
                for (int row = 0; row < Rows; row++)
                {
                    int index = row * Columns + col;
                    if (Grid[index] == tokenColor)
                    {
                        count++;
                        if (count == 4) return true;
                    }
                    else
                    {
                        count = 0;
                    }
                }
            }
            return false;
        }
        private bool CheckDiagonalWin(char tokenColor)
        {
            // Check diagonals from top-left to bottom-right
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int col = 0; col < Columns - 3; col++)
                {
                    int index = row * Columns + col;
                    if (Grid[index] == tokenColor &&
                        Grid[index + Columns + 1] == tokenColor &&
                        Grid[index + 2 * (Columns + 1)] == tokenColor &&
                        Grid[index + 3 * (Columns + 1)] == tokenColor)
                    {
                        return true;
                    }
                }
            }

            // Check diagonals from bottom-left to top-right
            for (int row = 3; row < Rows; row++)
            {
                for (int col = 0; col < Columns - 3; col++)
                {
                    int index = row * Columns + col;
                    if (Grid[index] == tokenColor &&
                        Grid[index - Columns + 1] == tokenColor &&
                        Grid[index - 2 * (Columns - 1)] == tokenColor &&
                        Grid[index - 3 * (Columns - 1)] == tokenColor)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsGridFull()
        {
            return !Grid.Contains('0');
        }

    }

}