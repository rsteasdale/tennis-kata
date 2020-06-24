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
        public void GivenPlayerWinsFirstPointThenUpdateScore(string winningPlayer, string expectedScore)
        {
            var game = new Game();
            game.WinPoint(winningPlayer);

            Assert.Equal(expectedScore, game.Score());
        }

        [Theory]
        [InlineData("Player 1", "30-Love")]
        [InlineData("Player 2", "Love-30")]
        public void GivenPlayerWinsFirstTwoPointsThenUpdateScore(string winningPlayer, string expectedScore)
        {
            var game = new Game();
            game.WinPoint(winningPlayer);
            game.WinPoint(winningPlayer);

            Assert.Equal(expectedScore, game.Score());
        }
    }

    public class Game
    {
        private readonly string _player1;
        private string _score = "Love-Love";
        private int _player2Score;
        private int _player1Score;

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
                _player1Score++;
            else
                _player2Score++;

            var player1Points = ConvertToPoints(_player1Score);
            var player2Points = ConvertToPoints(_player2Score);

            _score = $"{player1Points}-{player2Points}";
        }

        private static string ConvertToPoints(int shotsWon)
        {
            if (shotsWon == 0)
                return "Love";
            if (shotsWon == 1)
                return "15";
            return "30";
        }
    }
}
