using Xunit;
using Connect4.Models;

namespace Connect4Game.Tests
{
    public class GameTests
    {
        [Fact]
        public void GameInitializationTest()
        {
            var game = new Game
            {
                Host = new Player { Login = "Host", Password = "password" },
                Guest = new Player { Login = "Guest", Password = "password" }
            };

            Assert.NotNull(game);
            Assert.Equal("Awaiting Guest", game.Status);
        }

        [Fact]
        public void JoinGameTest()
        {
            var host = new Player { Login = "Host", Password = "password" };
            var guest = new Player { Login = "Guest", Password = "password" };
            var game = new Game { Host = host };

            game.JoinGame(guest);
            
            Assert.Equal(guest, game.Guest);
            Assert.Equal("In Progress", game.Status);
            Assert.Equal(guest, game.CurrentTurn);
        }

        [Fact]
        public void PlayTurnTest()
        {
            var host = new Player { Login = "Host", Password = "password" };
            var guest = new Player { Login = "Guest", Password = "password" };
            var game = new Game { Host = host };
            game.JoinGame(guest);

            game.PlayTurn(guest, 0);

            Assert.Equal("Yellow", game.Grid.Cells[5, 0].Token.Color);
            Assert.Equal(host, game.CurrentTurn);
        }

        [Fact]
        public void CheckWinConditionTest()
        {
            var host = new Player { Login = "Host", Password = "password" };
            var guest = new Player { Login = "Guest", Password = "password" };
            var game = new Game { Host = host };
            game.JoinGame(guest);

            game.PlayTurn(guest, 0);
            game.PlayTurn(host, 1);
            game.PlayTurn(guest, 0);
            game.PlayTurn(host, 1);
            game.PlayTurn(guest, 0);
            game.PlayTurn(host, 1);
            game.PlayTurn(guest, 0);

            Assert.Equal("Yellow", game.Grid.Cells[2, 0].Token.Color);
            Assert.Equal("Yellow", game.Grid.Cells[3, 0].Token.Color);
            Assert.Equal("Yellow", game.Grid.Cells[4, 0].Token.Color);
            Assert.Equal("Yellow", game.Grid.Cells[5, 0].Token.Color);

            Assert.True(game.CheckWinCondition());
            Assert.Equal("Finished", game.Status);
        }
    }
}