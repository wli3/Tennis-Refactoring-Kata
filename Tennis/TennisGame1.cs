using Monad;

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
            return
                (from m1 in TennisScoringRules.GetScoreIfEqual(_state)
                 from m2 in TennisScoringRules.GetScoreIfDeuce(m1)
                 from m3 in TennisScoringRules.GetScoreForNormalPlay(m2)
                 from m4 in TennisScoringRules.GetScoreIfPlayer1Advantage(m3)
                 from m5 in TennisScoringRules.GetScoreIfPlayer2Advantage(m4)
                 from m6 in TennisScoringRules.GetScoreIfPlayer1Win(m5)
                 from m7 in TennisScoringRules.GetScoreIfPlayer2Win(m6)
                 select m7)
                .Match(
                    Right: r => "Error",
                    Left: l => l
                )();
        }
    }
}