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
			string score = "";
			if (_score1 == _score2) {
				if (_score1 < 3) {
					score = ConvertScoreToString(_score1) + "-" + "All";
				}
				else {
					score = "Deuce";
				}
			}
			else if (_score1 >= 4 || _score2 >= 4) {
				var minusResult = _score1 - _score2;
				if (minusResult == 1) score = "Advantage player1";
				else if (minusResult == -1) score = "Advantage player2";
				else if (minusResult >= 2) score = "Win for player1";
				else score = "Win for player2";
			}
			else {
				score = ConvertScoreToString(_score1) + "-" + ConvertScoreToString(_score2);
			}
			return score;
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
