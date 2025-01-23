using Xunit;
using Connect4Game.Domain.Model;

namespace Connect4Game.Tests
{
    public class GameTests
    {
        [Fact]
        public void GameInitializationTest()
        {
            var game = new Game
            {
                Host = new Player { Id = "1", UserName = "Host" },
                HostId = "1",
                Name = "Test Game",
                CurrentTurnId = "1"
            };

            Assert.NotNull(game);
            Assert.Equal("Awaiting Guest", game.Status);
            Assert.Equal(new string('0', 42), game.Grid);
        }

        [Fact]
        public void JoinGameTest()
        {
            var host = new Player { Id = "1", UserName = "Host" };
            var guest = new Player { Id = "2", UserName = "Guest" };
            var game = new Game { Host = host, HostId = "1", Name = "Test Game", CurrentTurnId = "1" };

            game.JoinGame(guest);

            Assert.Equal(guest, game.Guest);
            Assert.Equal("2", game.GuestId);
            Assert.Equal("In Progress", game.Status);
            Assert.Equal(guest, game.CurrentTurn);
            Assert.Equal("2", game.CurrentTurnId);
        }

        [Fact]
        public void StartGameTest()
        {
            var host = new Player { Id = "1", UserName = "Host" };
            var guest = new Player { Id = "2", UserName = "Guest" };
            var game = new Game { Host = host, HostId = "1", Guest = guest, GuestId = "2", Name = "Test Game", CurrentTurnId = "1" };

            game.StartGame();

            Assert.Equal("In Progress", game.Status);
            Assert.Equal(guest, game.CurrentTurn);
            Assert.Equal("2", game.CurrentTurnId);
        }

        [Fact]
        public void PlayTurnTest()
        {
            var host = new Player { Id = "1", UserName = "Host" };
            var guest = new Player { Id = "2", UserName = "Guest" };
            var game = new Game { Host = host, HostId = "1", Guest = guest, GuestId = "2", Name = "Test Game", CurrentTurn = guest, CurrentTurnId = "2" };


            game.JoinGame(guest);
            game.StartGame();

            var result = game.PlayTurn(guest, 0);

            Assert.True(result);
            Assert.Equal('Y', game.Grid[35]);
            Assert.Equal(host, game.CurrentTurn);
            Assert.Equal("1", game.CurrentTurnId);
        }

        [Fact]
        public void PlayTurnInvalidTest()
        {
            var host = new Player { Id = "1", UserName = "Host" };
            var guest = new Player { Id = "2", UserName = "Guest" };
            var game = new Game { Host = host, HostId = "1", Guest = guest, GuestId = "2", Name = "Test Game", CurrentTurn = host, CurrentTurnId = "1" };

            game.JoinGame(guest);
            game.StartGame();

            var result = game.PlayTurn(guest, 7);

            Assert.False(result);
            Assert.Equal(new string('0', 42), game.Grid);
        }

        [Fact]
        public void CheckWinConditionTest()
        {
            var host = new Player { Id = "1", UserName = "Host" };
            var guest = new Player { Id = "2", UserName = "Guest" };
            var game = new Game { Host = host, HostId = "1", Guest = guest, GuestId = "2", Name = "Test Game", CurrentTurn = guest, CurrentTurnId = "2" };

            game.JoinGame(guest);
            game.StartGame();

            game.PlayTurn(guest, 0);
            game.PlayTurn(host, 1);
            game.PlayTurn(guest, 0);
            game.PlayTurn(host, 1);
            game.PlayTurn(guest, 0);
            game.PlayTurn(host, 1);
            game.PlayTurn(guest, 0);

            Assert.Equal("Finished", game.Status);
            Assert.Equal(guest, game.Winner);
        }

    }
}