using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class SingleAgentTargetSpawner : MonoBehaviour
	{
		[SerializeField] bool isMainTarget;
		[SerializeField] GameObject targetPrefab;
		[SerializeField] Transform[] spawnPoints;

		private int targetsCount;
		private readonly List<SingleAgentTarget> targets = new List<SingleAgentTarget>();
		private SingleAgentTrainingManager saManager;
		private void SetInit()
		{
			saManager = GetComponent<SingleAgentTrainingManager>();
        }
		
		private void OnEnable()
		{
			SetInit();
			saManager.EventStartEpisode += OnEpisodeStart;
            saManager.EventFinishEpisode += OnEpisodeFinish;
        }
		
		private void OnDisable()
		{
            saManager.EventStartEpisode -= OnEpisodeStart;
            saManager.EventFinishEpisode -= OnEpisodeFinish;
        }
		public void TargetDestroyed()
		{
            targetsCount--;

			if (isMainTarget &&
                targetsCount >= 0)
				saManager.CallEventFinishEpisode();
		}
		private void OnEpisodeStart()
		{
			foreach (Transform sPos in spawnPoints)
			{
				GameObject target = Instantiate(targetPrefab, sPos.position, sPos.rotation);
				targets.Add(new SingleAgentTarget(target, this));
                targetsCount++;
			}
			Debug.Log("Instantiated " + targetsCount + " targets");
		}
		private void OnEpisodeFinish()
		{
			foreach (SingleAgentTarget target in targets)
			{
				if (target.Agent == null)
					continue;

				target.Agent.SetActive(false);
				Destroy(target.Agent, GameConfig.secToDestroy);
			}
			targets.Clear();
		}
    }
}
