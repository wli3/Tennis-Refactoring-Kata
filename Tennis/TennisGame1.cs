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
			return Either<string, GameState>.Right(_state)
				.Chain(TennisScoringRules.GetScoreIfEqual)
				.Chain(TennisScoringRules.GetIfDeuce)
				.Chain(TennisScoringRules.GetNormalScore)
				.Chain(TennisScoringRules.GetIfPlayer1AdvantageScore)
				.Chain(TennisScoringRules.GetIfPlayer2AdvantageScore)
				.Chain(TennisScoringRules.GetScoreIfPlayer1Win)
				.Chain(TennisScoringRules.GetScoreIfPlayer2Win)
				.Left();
		}
	}
}
