using Xunit;
using Connect4.Models;

namespace Connect4Game.Tests
{
    public class GridTests
    {
        [Fact]
        public void DropTokenTest()
        {
            var grid = new Grid();
            var token = new Token { Color = "Red" };

            grid.DropToken(0, token);

            Assert.Equal(token, grid.Cells[5 * grid.Columns + 0].Token);
        }

        [Fact]
        public void IsFullTest()
        {
            var grid = new Grid();
            for (int col = 0; col < grid.Columns; col++)
            {
                for (int row = 0; row < grid.Rows; row++)
                {
                    grid.Cells[row * grid.Columns + col].Token = new Token { Color = "Red" };
                }
            }

            Assert.True(grid.IsFull());
        }

        [Fact]
        public void CheckHorizontalWinTest()
        {
            var grid = new Grid();
            var token = new Token { Color = "Red" };

            for (int col = 0; col < 4; col++)
            {
                grid.DropToken(col, token);
            }

            Assert.True(grid.CheckWinCondition());
        }

        [Fact]
        public void CheckVerticalWinTest()
        {
            var grid = new Grid();
            var token = new Token { Color = "Yellow" };
            var otherToken = new Token { Color = "Red" };

            for (int row = 0; row < 4; row++)
            {
                grid.DropToken(0, token);
                grid.DropToken(1, otherToken);
            }
            grid.PrintGrid();
            Console.WriteLine(grid.CheckWinCondition());
            Assert.True(grid.CheckWinCondition());
        }

        [Fact]
        public void CheckDiagonalWinTest()
        {
            var grid = new Grid();
            var token = new Token { Color = "Red" };
            var otherToken = new Token { Color = "Yellow" };
            grid.DropToken(0, token);
            grid.DropToken(1, otherToken);
            grid.DropToken(1, token);
            grid.DropToken(2, otherToken);
            grid.DropToken(2, otherToken);
            grid.DropToken(2, token);
            grid.DropToken(3, otherToken);
            grid.DropToken(3, otherToken);
            grid.DropToken(3, otherToken);
            grid.DropToken(3, token);


            Assert.True(grid.CheckWinCondition());
        }
        [Fact]
        public void CheckVerticalWinWithDifferentTokensTest()
        {
            var grid = new Grid();
            var token = new Token { Color = "Red" };
            var token2 = new Token { Color = "Red" };
            grid.DropToken(1, token);
            grid.DropToken(1, token2);
            grid.DropToken(1, token2);
            grid.DropToken(1, token2);

            Assert.True(grid.CheckWinCondition());
        }
    }
}