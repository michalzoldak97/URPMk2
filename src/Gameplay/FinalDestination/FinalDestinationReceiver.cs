using UnityEngine;

namespace URPMk2
{
	public class FinalDestinationReceiver : MonoBehaviour
	{
        [SerializeField] private GameScoreType scoreType;
        private float GeScore()
        {
            if (GetComponent<DamagableMaster>() == null)
                return 0f;

            return scoreType switch
            {
                GameScoreType.Bot => GetComponent<DamagableMaster>().GetHealth() * GameScore.BotHealthMultiplayer,
                GameScoreType.Cargo => GetComponent<DamagableMaster>().GetHealth() * GameScore.CargoHealthMultiplayer,
                _ => 0f,
            };
        }
        public void FinalDestinationReached()
        {
            GameScore.AddAttackersScore(GeScore());

            Destroy(gameObject, GameConfig.secToDestroy);
            gameObject.SetActive(false);
        }
    }
}
