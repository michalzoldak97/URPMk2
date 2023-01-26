
namespace URPMk2
{
	public static class GameScore
	{
		public const float CargoHealthMultiplayer = 0.8f;
		public const float BotHealthMultiplayer = 0.2f;
		public static float AttackersTeamScore { get; private set; }
		public static void AddAttackersScore(float toAdd)
		{
			AttackersTeamScore += toAdd;
		}
	}
}
