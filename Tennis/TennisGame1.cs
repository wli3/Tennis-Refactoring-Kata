using System;

namespace Tennis
{
	class TennisGame1 : ITennisGame
	{
		private GameState _state = GameState.GetInstance(0, 0);

		public void WonPoint(TennisPlayer tennisPlayer)
		{
			if (tennisPlayer == TennisPlayer.Player1)
				_state.Player1Score += 1;
			else
				_state.Player2Score += 1;
		}

		public string GetScore()
		{
			if (_state.Player1Score == _state.Player2Score && _state.Player1Score < 3)
			{
				return ConvertScoreToString(_state.Player1Score) + "-" + "All";
			}

			if (_state.Player1Score == _state.Player2Score)
			{
				return "Deuce";
			}

			if (_state.Player1Score < 4 && _state.Player2Score < 4)
			{
				return ConvertScoreToString(_state.Player1Score) + "-" + ConvertScoreToString(_state.Player2Score);
			}

			var minusResult = _state.Player1Score - _state.Player2Score;
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
