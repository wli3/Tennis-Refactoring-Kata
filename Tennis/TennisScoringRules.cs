using System;

namespace Tennis
{
	static class TennisScoringRules
	{
		public static Either<string, GameState> GetScoreIfEqual(GameState state)
		{
			if (state.Player1Score == state.Player2Score && state.Player1Score < 3)
			{
				return Either<string, GameState>.Left(ConvertScoreToString(state.Player1Score) + "-" + "All");
			}

			return Either<string, GameState>.Right(state);
		}

		public static Either<string, GameState> GetIfDeuce(GameState state)
		{
			if (state.Player1Score == state.Player2Score)
			{
				return Either<string, GameState>.Left("Deuce");
			}
			return Either<string, GameState>.Right(state);
		}

		public static Either<string, GameState> GetNormalScore(GameState state)
		{
			if (state.Player1Score < 4 && state.Player2Score < 4)
			{
				return
					Either<string, GameState>.Left(
						ConvertScoreToString(state.Player1Score) + "-" + ConvertScoreToString(state.Player2Score));
			}
			return Either<string, GameState>.Right(state);
		}

		public static Either<string, GameState> GetIfPlayer1AdvantageScore(GameState state)
		{
			var minusResult = state.Player1Score - state.Player2Score;
			if (minusResult == 1)
			{
				return Either<string, GameState>.Left("Advantage player1");
			}
			return Either<string, GameState>.Right(state);
		}

		public static Either<string, GameState> GetIfPlayer2AdvantageScore(GameState state)
		{
			var minusResult = state.Player1Score - state.Player2Score;
			if (minusResult == -1)
			{
				return Either<string, GameState>.Left("Advantage player2");
			}
			return Either<string, GameState>.Right(state);
		}

		public static Either<string, GameState> GetScoreIfPlayer1Win(GameState state)
		{
			var minusResult = state.Player1Score - state.Player2Score;
			if (minusResult >= 2)
			{
				return Either<string, GameState>.Left("Win for player1");
			}
			return Either<string, GameState>.Right(state);
		}

		public static Either<string, GameState> GetScoreIfPlayer2Win(GameState state)
		{
			var minusResult = state.Player1Score - state.Player2Score;
			if (minusResult <= -2) {
				return Either<string, GameState>.Left("Win for player2");
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