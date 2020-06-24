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

        [Theory]
        [InlineData("Player 1", "Player 1 Wins!")]
        [InlineData("Player 2", "Player 2 Wins!")]
        public void GivenPlayer1WinsAdvantageThenTheyWinTheGame(string winningPlayer, string expectedScore)
        {
            var game = new Game();
            game.WinPoint("Player 1");
            game.WinPoint("Player 1");
            game.WinPoint("Player 1");
            game.WinPoint("Player 2");
            game.WinPoint("Player 2");
            game.WinPoint("Player 2");

            game.WinPoint(winningPlayer);
            game.WinPoint(winningPlayer);

            Assert.Equal(expectedScore, game.Score());
        }
    }
}
