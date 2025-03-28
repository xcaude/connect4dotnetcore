using System.Data.Common;
using Microsoft.AspNetCore.Mvc;

using Connect4Game.ClientWeb.Models;

namespace Connect4Game.ClientWeb.Models
{   
    /// <summary>
    /// Represents a cell in the Connect 4 grid.
    /// </summary>
    public class Grid
    {
        public int Id { get; set; }
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
        public List<Cell> Cells { get; set; } = new List<Cell>();

        public Grid()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Cells.Add(new Cell { Row = row, Column = col, Grid = this });
                }
            }
        }
        /// <summary>
        /// Drops a token in the specified column.
        /// </summary>
        /// <param name="column">Column in which the token will be droped</param>
        /// <param name="token">Token to drop</param>
        public bool DropToken(int column, Token token)
        {

            for (int row = Rows - 1; row >= 0; row--)
            {
                if (Cells[row * Columns + column].Token == null)
                {
                    Cells[row * Columns + column].Token = token;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if the grid is full.
        /// </summary>
        /// <returns>True if full else false</returns>
        public bool IsFull()
        {
            for (int col = 0; col < Columns; col++)
            {
                if (Cells[col].Token == null)
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
                    var currentToken = Cells[row * Columns + col].Token;

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
                        lastColor = currentToken?.Color!;
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
                    var currentToken = Cells[row * Columns + col].Token;

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
                        lastColor = currentToken?.Color!;
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
                    if (Cells[row * Columns + col].Token != null &&
                        Cells[(row + 1) * Columns + (col + 1)].Token?.Color == Cells[row * Columns + col].Token.Color &&
                        Cells[(row + 2) * Columns + (col + 2)].Token?.Color == Cells[row * Columns + col].Token.Color &&
                        Cells[(row + 3) * Columns + (col + 3)].Token?.Color == Cells[row * Columns + col].Token.Color)
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
                    if (Cells[row * Columns + col].Token != null &&
                        Cells[(row + 1) * Columns + (col - 1)].Token?.Color == Cells[row * Columns + col].Token.Color &&
                        Cells[(row + 2) * Columns + (col - 2)].Token?.Color == Cells[row * Columns + col].Token.Color &&
                        Cells[(row + 3) * Columns + (col - 3)].Token?.Color == Cells[row * Columns + col].Token.Color)
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
                    var token = Cells[row * Columns + col].Token;
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