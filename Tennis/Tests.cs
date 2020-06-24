using System;
using System.Collections.Generic;
using Xunit;

namespace Tennis
{
    public class Tests
    {
        [Fact]
        public void GivenNewGameThenScoreIsLoveLove()
        {
            var game = new Game();

            Assert.Equal("Love-Love", game.Score());
        }

        [Theory]
        [InlineData("Player 1","15-Love")]
        [InlineData("Player 2","Love-15")]
        public void GivenPlayer1WinsFirstPointThenUpdateScore(string winningPlayer, string expectedScore)
        {
            var game = new Game();
            game.WinPoint(winningPlayer);

            Assert.Equal(expectedScore, game.Score());
        }
    }

    public class Game
    {
        private readonly string _player1;
        private string _score = "Love-Love";

        public Game(string player1 = "Player 1")
        {
            _player1 = player1;
        }

        public string Score()
        {
            return _score;
        }

        public void WinPoint(string player)
        {
            if (player == _player1)
                _score = "15-Love";
            else
                _score = "Love-15";
        }
    }
}
