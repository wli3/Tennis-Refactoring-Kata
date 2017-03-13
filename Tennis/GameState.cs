namespace Tennis
{
	struct GameState
	{
		static public GameState GetInstance(int player1Score, int player2Score)
		{
			return new GameState(player1Score, player2Score);
		}

		private GameState(int player1Score, int player2Score)
		{
			Player1Score = player1Score;
			Player2Score = player2Score;
		}

		public int Player1Score;
		public int Player2Score;
	}
}