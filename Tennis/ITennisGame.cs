namespace Tennis
{
	public interface ITennisGame
	{
		void WonPoint(TennisPlayer tennisPlayer);
		string GetScore();
	}
}
