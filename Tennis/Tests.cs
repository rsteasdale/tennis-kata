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

        [Theory]
        [InlineData("Player 1", "40-Love")]
        [InlineData("Player 2", "Love-40")]
        public void GivenPlayerWinsFirstThreePointsThenUpdateScore(string winningPlayer, string expectedScore)
        {
            var game = new Game();
            game.WinPoint(winningPlayer);
            game.WinPoint(winningPlayer);
            game.WinPoint(winningPlayer);

            Assert.Equal(expectedScore, game.Score());
        }

        [Theory]
        [InlineData("Player 1", "Player 1 Wins!")]
        [InlineData("Player 2", "Player 2 Wins!")]
        public void GivenPlayerWins40LoveThenTheyWin(string winningPlayer, string expectedScore)
        {
            var game = new Game();
            game.WinPoint(winningPlayer);
            game.WinPoint(winningPlayer);
            game.WinPoint(winningPlayer);
            game.WinPoint(winningPlayer);

            Assert.Equal(expectedScore, game.Score());
        }

        [Fact]
        public void GivenBothPlayersHave40ThenDeuce()
        {
            var game = new Game();
            game.WinPoint("Player 1");
            game.WinPoint("Player 1");
            game.WinPoint("Player 1");
            game.WinPoint("Player 2");
            game.WinPoint("Player 2");
            game.WinPoint("Player 2");

            Assert.Equal("Deuce", game.Score());
        }

        [Theory]
        [InlineData("Player 1", "Advantage Player 1")]
        [InlineData("Player 2", "Advantage Player 2")]
        public void GivenPlayerWinsDeuceThenAdvantage(string winningPlayer, string expectedScore)
        {
            var game = new Game();
            game.WinPoint("Player 1");
            game.WinPoint("Player 1");
            game.WinPoint("Player 1");
            game.WinPoint("Player 2");
            game.WinPoint("Player 2");
            game.WinPoint("Player 2");

            game.WinPoint(winningPlayer);

            Assert.Equal(expectedScore, game.Score());
        }

        [Theory]
        [InlineData("Player 1", "Player 2")]
        [InlineData("Player 2", "Player 1")]
        public void GivenPlayerLosesAdvantageThenBackToDeuce(string winningPlayer, string losingPlayer)
        {
            var game = new Game();
            game.WinPoint("Player 1");
            game.WinPoint("Player 1");
            game.WinPoint("Player 1");
            game.WinPoint("Player 2");
            game.WinPoint("Player 2");
            game.WinPoint("Player 2");

            game.WinPoint(winningPlayer);
            game.WinPoint(losingPlayer);

            Assert.Equal("Deuce", game.Score());
        }
    }

    public class Game
    {
        private readonly string _player1;
        private readonly string _player2;
        private string _score = "Love-Love";
        private int _player2Score;
        private int _player1Score;

        private const int FortyPoints = 3;

        public Game(string player1 = "Player 1", string player2 = "Player 2")
        {
            _player1 = player1;
            _player2 = player2;
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

            if (IsDeuce())
                _score = "Deuce";
            else if (PlayersHaveAtLeastForty() && PlayerWinningByOne())
                _score = $"Advantage {GetWinningPlayer()}";
            else if (PlayersHaveAtLeastForty() && _player1Score == _player2Score)
                _score = "Deuce";
            else if (PlayerHasWon())
                _score = $"{player} Wins!";
            else
                _score = $"{ConvertToPoints(_player1Score)}-{ConvertToPoints(_player2Score)}";
        }

        private string GetWinningPlayer()
        {
            return _player1Score > _player2Score ? _player1 : _player2;
        }

        private bool PlayerWinningByOne()
        {
            return _player1Score == _player2Score + 1 || _player2Score == _player1Score + 1;
        }

        private bool PlayersHaveAtLeastForty()
        {
            return _player1Score >= FortyPoints && _player2Score >= FortyPoints;
        }


        private bool IsDeuce()
        {
            return _player1Score == FortyPoints && _player2Score == FortyPoints;
        }

        private bool PlayerHasWon()
        {
            return _player1Score > FortyPoints || _player2Score > FortyPoints;
        }

        private static string ConvertToPoints(int shotsWon)
        {
            switch (shotsWon)
            {
                case 1:
                    return "15";
                case 2:
                    return "30";
                case 3:
                    return "40";
                default:
                    return "Love";
            }
        }
    }
}
