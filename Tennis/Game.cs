namespace Tennis
{
    public class Game
    {
        private readonly string _player1;
        private readonly string _player2;
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
            if (IsDeuce())
                return "Deuce";
            if (PlayersHaveAtLeastForty() && PlayerWinningByOne())
                return $"Advantage {GetWinningPlayer()}";
            if (PlayersHaveAtLeastForty() && _player1Score == _player2Score)
                return "Deuce";
            if (PlayerHasWon())
                return $"{GetWinningPlayer()} Wins!";
            
            return $"{ConvertToPoints(_player1Score)}-{ConvertToPoints(_player2Score)}";
        }

        public void WinPoint(string player)
        {
            if (player == _player1)
                _player1Score++;
            else
                _player2Score++;
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