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
			return OldEither<string, GameState>.Right(_state)
				.Chain(TennisScoringRules.GetScoreIfEqual)
				.Chain(TennisScoringRules.GetScoreIfDeuce)
				.Chain(TennisScoringRules.GetScoreForNormalPlay)
				.Chain(TennisScoringRules.GetScoreIfPlayer1Advantage)
				.Chain(TennisScoringRules.GetScoreIfPlayer2Advantage)
				.Chain(TennisScoringRules.GetScoreIfPlayer1Win)
				.Chain(TennisScoringRules.GetScoreIfPlayer2Win)
				.Left();
		}
	}
}
