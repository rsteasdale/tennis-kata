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
    }

    public class Game
    {
        public string Score()
        {
            return "Love-Love";
        }
    }
}
