using System;

namespace Tennis
{
	static class TennisScoringRules
	{
		public static OldEither<string, GameState> GetScoreIfEqual(GameState state)
		{
			if (state.Player1Score == state.Player2Score && state.Player1Score < 3)
			{
				return OldEither<string, GameState>.Left(ConvertScoreToString(state.Player1Score) + "-" + "All");
			}

			return OldEither<string, GameState>.Right(state);
		}

		public static OldEither<string, GameState> GetScoreIfDeuce(GameState state)
		{
			if (state.Player1Score == state.Player2Score)
			{
				return OldEither<string, GameState>.Left("Deuce");
			}
			return OldEither<string, GameState>.Right(state);
		}

		public static OldEither<string, GameState> GetScoreForNormalPlay(GameState state)
		{
			if (state.Player1Score < 4 && state.Player2Score < 4)
			{
				return
					OldEither<string, GameState>.Left(
						ConvertScoreToString(state.Player1Score) + "-" + ConvertScoreToString(state.Player2Score));
			}
			return OldEither<string, GameState>.Right(state);
		}

		public static OldEither<string, GameState> GetScoreIfPlayer1Advantage(GameState state)
		{
			var minusResult = state.Player1Score - state.Player2Score;
			if (minusResult == 1)
			{
				return OldEither<string, GameState>.Left("Advantage player1");
			}
			return OldEither<string, GameState>.Right(state);
		}

		public static OldEither<string, GameState> GetScoreIfPlayer2Advantage(GameState state)
		{
			var minusResult = state.Player1Score - state.Player2Score;
			if (minusResult == -1)
			{
				return OldEither<string, GameState>.Left("Advantage player2");
			}
			return OldEither<string, GameState>.Right(state);
		}

		public static OldEither<string, GameState> GetScoreIfPlayer1Win(GameState state)
		{
			var minusResult = state.Player1Score - state.Player2Score;
			if (minusResult >= 2)
			{
				return OldEither<string, GameState>.Left("Win for player1");
			}
			return OldEither<string, GameState>.Right(state);
		}

		public static OldEither<string, GameState> GetScoreIfPlayer2Win(GameState state)
		{
			var minusResult = state.Player1Score - state.Player2Score;
			if (minusResult <= -2) {
				return OldEither<string, GameState>.Left("Win for player2");
			}
			return OldEither<string, GameState>.Right(state);
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