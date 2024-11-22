namespace Connect4Game.Models
{   
    /// <summary>
    /// Represents a cell in the Connect 4 grid.
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Gets or sets the number of rows in the grid.
        /// </summary>
        public int Rows { get; set; } = 6;
        /// <summary>
        /// Gets or sets the number of columns in the grid.
        /// </summary>
        public int Columns { get; set; } = 7;
        /// <summary>
        /// Gets or sets the cells in the grid.
        /// </summary>
        public Cell[,] Cells { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        public Grid()
        {
            Cells = new Cell[Rows, Columns];
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Cells[row, col] = new Cell { Row = row, Column = col };
                }
            }
        }
        /// <summary>
        /// Drops a token in the specified column.
        /// </summary>
        /// <param name="column">Column in which the token will be droped</param>
        /// <param name="token">Token to drop</param>
        public void DropToken(int column, Token token)
        {
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (Cells[row, column].Token == null)
                {
                    Cells[row, column].Token = token;
                    break;
                }
            }
        }
        /// <summary>
        /// Checks if the grid is full.
        /// </summary>
        /// <returns>True if full else false</returns>
        public bool IsFull()
        {
            for (int col = 0; col < Columns; col++)
            {
                if (Cells[0, col].Token == null)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Checks the win conditions of the grid.
        /// </summary>
        /// <returns>True if won else false</returns>
        public bool CheckWinCondition()
        {
            return CheckHorizontalWin() || CheckVerticalWin() || CheckDiagonalWin();
        }
        /// <summary>
        /// Checks for a horizontal win in the grid.
        /// </summary>
        /// <returns>True if horizontal win else false</returns>
        private bool CheckHorizontalWin()
        {
            for (int row = 0; row < Rows; row++)
            {
                int count = 0;
                string lastColor = null;

                for (int col = 0; col < Columns; col++)
                {
                    var currentToken = Cells[row, col].Token;

                    if (currentToken != null && currentToken.Color == lastColor)
                    {
                        count++;
                        if (count == 4)
                        {
                            Console.WriteLine($"Horizontal win found at row {row}");
                            return true;
                        }
                    }
                    else
                    {
                        count = 1;
                        lastColor = currentToken?.Color;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Checks for a vertical win in the grid.
        /// </summary>
        /// <returns>True if vertical win else false</returns>
        private bool CheckVerticalWin()
        {
            for (int col = 0; col < Columns; col++)
            {
                int count = 0;
                string lastColor = null;

                for (int row = 0; row < Rows; row++)
                {
                    var currentToken = Cells[row, col].Token;

                    if (currentToken != null && currentToken.Color == lastColor)
                    {
                        count++;
                        if (count == 4)
                        {
                            Console.WriteLine($"Vertical win found at column {col}");
                            return true;
                        }
                    }
                    else
                    {
                        count = 1;
                        lastColor = currentToken?.Color;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Checks for a diagonal win in the grid.
        /// </summary>
        /// <returns>True if diagonal win else false</returns>
        private bool CheckDiagonalWin()
        {
            // Check diagonals from top-left to bottom-right
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int col = 0; col < Columns - 3; col++)
                {
                    if (Cells[row, col].Token != null &&
                        Cells[row, col].Token.Color == Cells[row + 1, col + 1].Token?.Color &&
                        Cells[row, col].Token.Color == Cells[row + 2, col + 2].Token?.Color &&
                        Cells[row, col].Token.Color == Cells[row + 3, col + 3].Token?.Color)
                    {
                        Console.WriteLine($"Diagonal win (positive slope) found starting at ({row}, {col})");
                        return true;
                    }
                }
            }
            // Check diagonals from top-right to bottom-left
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int col = 3; col < Columns; col++)
                {
                    if (Cells[row, col].Token != null &&
                        Cells[row, col].Token.Color == Cells[row + 1, col - 1].Token?.Color &&
                        Cells[row, col].Token.Color == Cells[row + 2, col - 2].Token?.Color &&
                        Cells[row, col].Token.Color == Cells[row + 3, col - 3].Token?.Color)
                    {
                        Console.WriteLine($"Diagonal win (negative slope) found starting at ({row}, {col})");
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Prints the grid to the console.
        /// </summary>
        public void PrintGrid()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    var token = Cells[row, col].Token;
                    if (token == null)
                    {
                        Console.Write("x");
                    }
                    else
                    {
                        Console.Write(token.Color[0]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}