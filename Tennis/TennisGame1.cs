using System;

namespace Tennis
{
	class TennisGame1 : ITennisGame
	{
		private int _score1 = 0;
		private int _score2 = 0;

		public void WonPoint(TennisPlayer tennisPlayer)
		{
			if (tennisPlayer == TennisPlayer.Player1)
				_score1 += 1;
			else
				_score2 += 1;
		}

		public string GetScore()
		{
			if (_score1 == _score2 && _score1 < 3)
			{
				return ConvertScoreToString(_score1) + "-" + "All";
			}

			if (_score1 == _score2)
			{
				return "Deuce";
			}

			if (_score1 < 4 && _score2 < 4)
			{
				return ConvertScoreToString(_score1) + "-" + ConvertScoreToString(_score2);
			}

			var minusResult = _score1 - _score2;
			if (minusResult == 1)
			{
				return "Advantage player1";
			}
			if (minusResult == -1)
			{
				return "Advantage player2";
			}
			if (minusResult >= 2)
				return "Win for player1";

			return "Win for player2";
		}

		private static string ConvertScoreToString(int score)
		{
			switch (score) {
				case 0:
					return "Love";
				case 1:
					return "Fifteen";
				case 2:
					return "Thirty";
				case 3:
					return "Forty";
			}
			throw new ArgumentException("score");
		}
	}
}
