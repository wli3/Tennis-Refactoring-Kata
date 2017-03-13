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
			var result = Either<string, GameState>.Right(_state)
				.Chain(GetScoreIfEqual)
				.Chain(GetIfDeuce)
				.Chain(GetNormalScore);
			if (result.IsLeft())
			{
				return result.Left();
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

		static private Either<string, GameState> GetScoreIfEqual(GameState state)
		{
			if (state.Player1Score == state.Player2Score && state.Player1Score < 3)
			{
				return Either<string, GameState>.Left(ConvertScoreToString(state.Player1Score) + "-" + "All");
			}

			return Either<string, GameState>.Right(state);
		}

		private static Either<string, GameState> GetIfDeuce(GameState state)
		{
			if (state.Player1Score == state.Player2Score)
			{
				return Either<string, GameState>.Left("Deuce");
			}
			return Either<string, GameState>.Right(state);
		}

		private static Either<string, GameState> GetNormalScore(GameState state)
		{
			if (state.Player1Score < 4 && state.Player2Score < 4)
			{
				return
					Either<string, GameState>.Left(
						ConvertScoreToString(state.Player1Score) + "-" + ConvertScoreToString(state.Player2Score));
			}
			return Either<string, GameState>.Right(state);
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
