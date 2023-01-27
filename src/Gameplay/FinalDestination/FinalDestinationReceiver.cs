using SD;
using UnityEngine;

namespace URPMk2
{
	public class FinalDestinationReceiver : MonoBehaviour, IFinalDestinationEnjoyer
	{
        [SerializeField] private GameScoreType scoreType;
        private float GetScore()
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
            float score = GetScore();
            if (score == 0f)
                return;

            GameScore.AddAttackersScore(score);

            if (GetComponent<SquadCargoMaster>() != null) // squad training chack, don't do this at home
                GetComponent<SquadCargoMaster>().OnTargetReached();

            Destroy(gameObject, GameConfig.secToDestroy);
            GetComponent<DamagableMaster>().CallEventDestroyObject(null);
            gameObject.SetActive(false);
        }
    }
}
